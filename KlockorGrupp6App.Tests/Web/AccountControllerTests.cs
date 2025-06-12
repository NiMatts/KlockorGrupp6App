using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Dtos;
using KlockorGrupp6App.Application.Users;
using KlockorGrupp6App.Domain;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Tests.Helpers;
using KlockorGrupp6App.Web.Controllers;
using KlockorGrupp6App.Web.Views.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;


namespace KlockorGrupp6App.Tests.Web
{
    public class AccountControllerTests
    {
        #region [Test Setup]
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IClockService> _mockClockService;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;

        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            // Creating mocks for UserManager, IUserService and IClockService with the TestHelper class
            _controller = TestHelper.CreateAccountController(out _mockUserManager, out _mockUserService, out _mockClockService);

            // Creating a mocked logged-in user
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, "testuser@example.com"),
            }, "mock"));

            // Connecting the mocked user to the controllers Httpcontext
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }
        #endregion

        [Trait("AccountController", "Members")]
        [Fact]
        public async Task Members_ReturnsViewWithClocksForUser()
        {
            var appUser = new ApplicationUser { Id = "user-id", Email = "testuser@example.com" };
            var clocks = new List<Clock>
        {
            new() { Id = 1, Brand = "Rolex" },
            new() { Id = 2, Brand = "Omega" }
        };

            _mockUserManager.Setup(m => m.FindByEmailAsync("testuser@example.com"))
                .ReturnsAsync(appUser);

            _mockClockService.Setup(s => s.GetAllByUserIdAsync("user-id"))
                .Returns(Task.FromResult(clocks.ToArray()));

            var result = await _controller.Members();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<MembersVM>(viewResult.Model);
            Assert.Equal(2, model.ClocksItems.Length);
        }

        [Trait("AccountController", "Admin")]
        [Fact]
        public async Task Admin_ReturnsViewWithGroupedClocksByUser()
        {
            // Arrange
            var clocks = new List<Clock>
    {
        new() { Id = 1, Brand = "Rolex", CreatedByUserID = "user1" },
        new() { Id = 2, Brand = "Omega", CreatedByUserID = "user1" },
        new() { Id = 3, Brand = "Seiko", CreatedByUserID = "user2" }
    };

            var user1 = new ApplicationUser { Id = "user1", Email = "user1@example.com" };
            var user2 = new ApplicationUser { Id = "user2", Email = "user2@example.com" };

            _mockClockService.Setup(s => s.GetAllAsync())
                .Returns(Task.FromResult(clocks.ToArray()));

            _mockUserManager.Setup(m => m.FindByIdAsync("user1"))
                .ReturnsAsync(user1);

            _mockUserManager.Setup(m => m.FindByIdAsync("user2"))
                .ReturnsAsync(user2);

            // Act
            var result = await _controller.Admin();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<AdminVM>>(viewResult.Model);

            Assert.Equal(2, model.Count); // Two groups for two users

            var firstGroup = model.FirstOrDefault(g => g.UserId == "user1");
            Assert.NotNull(firstGroup);
            Assert.Equal("user1@example.com", firstGroup.UserEmail);
            Assert.Equal(2, firstGroup.ClocksItems.Count);
        }

        [Trait("AccountController", "Register-GET")]
        [Fact]
        public void Register_GET_ReturnsView()
        {
            var result = _controller.Register();

            Assert.IsType<ViewResult>(result);
        }

        [Trait("AccountController", "Register-POST")]
        [Fact]
        public async Task Register_POST_ValidModel_RedirectsToLogin()
        {
            // Arrange
            var registerVM = new RegisterVM
            {
                Email = "newuser@example.com",
                FirstName = "First",
                LastName = "Last",
                Password = "Password123!",
                isAdmin = false
            };

            _mockUserService.Setup(s => s.CreateUserAsync(
                It.IsAny<UserProfileDto>(),
                It.IsAny<string>(),
                It.IsAny<bool>()))
                .ReturnsAsync(new UserResultDto(null)); //

            // Act
            var result = await _controller.RegisterAsync(registerVM);

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(_controller.Login), redirect.ActionName);
        }

        [Trait("AccountController", "Register-POST")]
        [Fact]
        public async Task Register_POST_InvalidModel_ReturnsView()
        {
            _controller.ModelState.AddModelError("Email", "Required");

            var registerVM = new RegisterVM(); // empty/invalid

            var result = await _controller.RegisterAsync(registerVM);

            Assert.IsType<ViewResult>(result);
        }

        [Trait("AccountController", "Register-POST")]
        [Fact]
        public async Task Register_POST_UserCreationFails_ReturnsViewWithError()
        {
            var registerVM = new RegisterVM
            {
                Email = "newuser@example.com",
                FirstName = "First",
                LastName = "Last",
                Password = "Password123!",
                isAdmin = false
            };

            _mockUserService.Setup(s => s.CreateUserAsync(
                It.IsAny<UserProfileDto>(),
                It.IsAny<string>(),
                It.IsAny<bool>()))
                .ReturnsAsync(new UserResultDto("Error creating user"));

            var result = await _controller.RegisterAsync(registerVM);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(_controller.ModelState.IsValid);
            Assert.True(_controller.ModelState.ContainsKey(string.Empty));
        }

        [Trait("AccountController", "Login-GET")]
        [Fact]
        public void Login_GET_ReturnsView()
        {
            var result = _controller.Login();

            Assert.IsType<ViewResult>(result);
        }

        [Trait("AccountController", "Login-POST")]
        [Fact]
        public async Task Login_POST_ValidCredentials_AdminRedirectsToAdmin()
        {
            var loginVM = new LoginVM 
            { 
                Username = "admin@example.com", 
                Password = "password" 
            };

            var appUser = new ApplicationUser { Email = loginVM.Username };

            _mockUserService.Setup(s => s.SignInAsync(loginVM.Username, loginVM.Password))
                .ReturnsAsync(new UserResultDto(null)); // Successful sign-in

            _mockUserManager.Setup(m => m.FindByEmailAsync(loginVM.Username))
                .ReturnsAsync(appUser);

            _mockUserManager.Setup(m => m.GetRolesAsync(appUser))
                .ReturnsAsync(new List<string> { "Administrator" });

            var result = await _controller.LoginAsync(loginVM);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(_controller.Admin), redirect.ActionName);
        }

        [Trait("AccountController", "Login-POST")]
        [Fact]
        public async Task Login_POST_ValidCredentials_NonAdminRedirectsToMembers()
        {
            var loginVM = new LoginVM 
            { 
                Username = "user@example.com", 
                Password = "password" 
            };

            var appUser = new ApplicationUser { Email = loginVM.Username };

            _mockUserService.Setup(s => s.SignInAsync(loginVM.Username, loginVM.Password))
                .ReturnsAsync(new UserResultDto(null));
                        
            _mockUserManager.Setup(m => m.FindByEmailAsync(loginVM.Username))
                .ReturnsAsync(appUser);

            _mockUserManager.Setup(m => m.GetRolesAsync(appUser))
                .ReturnsAsync(new List<string>());

            var result = await _controller.LoginAsync(loginVM);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(_controller.Members), redirect.ActionName);
        }

        [Trait("AccountController", "Login-POST")]
        [Fact]
        public async Task Login_POST_InvalidModel_ReturnsView()
        {
            _controller.ModelState.AddModelError("Username", "Required");

            var loginVM = new LoginVM();

            var result = await _controller.LoginAsync(loginVM);

            Assert.IsType<ViewResult>(result);
        }

        [Trait("AccountController", "Login-POST")]
        [Fact]
        public async Task Login_POST_InvalidCredentials_ReturnsViewWithError()
        {
            var loginVM = new LoginVM 
            { 
                Username = "user@example.com", 
                Password = "wrongpassword" 
            };

            _mockUserService.Setup(s => s.SignInAsync(loginVM.Username, loginVM.Password))
                .ReturnsAsync(new UserResultDto("Invalid credentials"));

            var result = await _controller.LoginAsync(loginVM);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(_controller.ModelState.IsValid);
            Assert.True(_controller.ModelState.ContainsKey(string.Empty));
        }

        [Trait("AccountController", "Logout")]
        [Fact]
        public async Task Logout_CallsSignOutAndRedirectsToLogin()
        {
            var result = await _controller.Logout();

            _mockUserService.Verify(s => s.SignOutAsync(), Times.Once);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(_controller.Login), redirect.ActionName);
        }

    }
}

