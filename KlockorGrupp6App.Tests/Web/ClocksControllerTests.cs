using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Domain;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Tests.Helpers;
using KlockorGrupp6App.Web.Controllers;
using KlockorGrupp6App.Web.Views.Clocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace KlockorGrupp6App.Tests.Web
{
    public class ClocksControllerTests
    {
        #region [Test Setup]        
        // Mocking dependencies for the controller
        private readonly Mock<IClockService> _mockClockService;        
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        // The controller we're testing
        private readonly ClocksController _controller;

        public ClocksControllerTests()
        {
            // Fetching the mocked ClockService and UserManager using the TestHelper class
            _controller = TestHelper.CreateClocksController(
                out _mockClockService, 
                out _mockUserManager);
        }
        #endregion

        [Trait("ClocksController", "Index")]
        [Fact]
        public async Task Index_ShouldReturnViewWithCorrectData()
        {
            // Arrange: Creating a list with 2 clock-objects
            var clocks = new[]
            {
                new Clock { Id = 1, Brand = "Rolex", Model = "Submariner" },
                new Clock { Id = 2, Brand = "Omega", Model = "Speedmaster" }
            };

            // Setting up the mock service so when GetAll() is called, it will "pretend" to return the clocks list we created above.
            _mockClockService.Setup(s => s.GetAllAsync()).ReturnsAsync(clocks);

            // Act: Calling the Index method on the controller
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result); // Checking that the result is not null
            var model = Assert.IsType<IndexVM>(result.Model); // Checking that the model is of type IndexVM
            Assert.Equal(2, model.ClocksItems.Length); // Checking that the model contains 2 clock items
            Assert.Equal("Rolex", model.ClocksItems[0].Brand); // Checking that the first clock's brand is "Rolex"
        }


        [Trait("ClocksController", "Details")]
        [Fact]
        public async Task Details_ShouldReturnCorrectClockData()
        {
            // Arrange
            var clock = new Clock { Id = 1, Brand = "Casio", Model = "G-Shock", Price = 1000, Year = new DateTime(2022, 1, 1) };

            var controller = TestHelper.CreateClocksController(out var mockService, out _);
            mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(clock);

            // Act
            var result = await controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<DetailsVM>(viewResult.Model);
            Assert.Equal("Casio", model.Brand);
            Assert.Equal("G-Shock", model.Model);
        }


        [Trait("ClocksController", "Create-GET")]
        [Fact]
        public void Create_Get_ShouldReturnView()
        {
            var controller = TestHelper.CreateClocksController(out _, out _);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }


        [Trait("ClocksController", "Create-POST")]
        [Fact]
        public async Task Create_Post_ShouldAddClockAndRedirect_WhenModelIsValid()
        {
            // Arrange
            var controller = TestHelper.CreateClocksController(out var mockService, out var mockUserManager);
            var user = new ApplicationUser { Id = "abc123", Email = "test@example.com" };

            var viewModel = new CreateVM
            {
                Brand = "Seiko",
                Model = "Prospex",
                Price = 150000,
                Year = 2020
            };

            mockUserManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

            // Simulera en autentiserad användare
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity(
                        [new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Email)]))
                }
            };

            // Act
            var result = await controller.Create(viewModel);

            // Assert
            mockService.Verify(s => s.AddAsync(It.IsAny<Clock>()), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }


        [Trait("ClocksController", "Create-POST")]
        [Fact]
        public async Task Create_Post_ShouldReturnView_WhenModelStateIsInvalid()
        {
            var controller = TestHelper.CreateClocksController(out var mockService, out _);
            controller.ModelState.AddModelError("Brand", "Required");

            var viewModel = new CreateVM
                { 
                Brand = "Seiko", 
                Model = "Prospex", 
                Price = 1500, 
                Year = 2020
            };

            var result = await controller.Create(viewModel);

            Assert.IsType<ViewResult>(result);
            mockService.Verify(s => s.AddAsync(It.IsAny<Clock>()), Times.Never);
        }


        [Trait("ClocksController", "Delete")]
        [Fact]
        public async Task Delete_ShouldRemoveClockAndRedirectToIndex()
        {
            // Arrange
            var clock = new Clock { Id = 1, Brand = "Timex" };

            var controller = TestHelper.CreateClocksController(out var mockService, out _);
            mockService.Setup(s => s.GetByIdAsync(clock.Id)).ReturnsAsync(clock);

            // Act
            var result = await controller.Delete(clock.Id);

            // Assert
            mockService.Verify(s => s.RemoveAsync(clock), Times.Once);
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

    }
}
