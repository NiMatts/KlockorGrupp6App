using KlockorGrupp6App.Application;
using KlockorGrupp6App.Application.Dtos;
using KlockorGrupp6App.Application.Users;
using KlockorGrupp6App.Tests.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockorGrupp6App.Tests.Application
{
    public class UserServiceTests
    {
        private readonly Mock<IIdentityUserService> _mockIdentityUserService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userService = TestHelper.CreateUserServiceWithMocks(
                out _mockIdentityUserService,
                out _mockUnitOfWork);
        }


        [Trait("UserService", "CreateUser")]
        [Fact]
        public async Task CreateUserAsync_ReturnsUserResult()
        {
            var userDto = new UserProfileDto(
                "Test@test.com",
                "Test",
                "User"
                );

            var expected = new UserResultDto(null);

            _mockIdentityUserService.Setup(s => s.CreateUserAsync(userDto, "password123", false))
                .ReturnsAsync(expected);

            var result = await _userService.CreateUserAsync(userDto, "password123", false);

            Assert.Equal(expected, result);
        }


        [Trait("UserService", "SignIn")]
        [Fact]
        public async Task SignInAsync_ReturnsUserResult()
        {
            var expected = new UserResultDto(null);

            _mockIdentityUserService.Setup(s => s.SignInAsync("test@example.com", "Qwerty!"))
                .ReturnsAsync(expected);

            var result = await _userService.SignInAsync("test@example.com", "Qwerty!");

            Assert.Equal(expected, result);
        }


        [Trait("UserService", "SignOut")]
        [Fact]
        public async Task SignOutAsync_CallsSignOut()
        {
            _mockIdentityUserService.Setup(s => s.SignOutAsync())
                .Returns(Task.CompletedTask)
                .Verifiable();

            await _userService.SignOutAsync();

            _mockIdentityUserService.Verify(s => s.SignOutAsync(), Times.Once);
        }


        [Trait("UserService", "GetAllUsers")]
        [Fact]
        public async Task GetAllUsersAsync_ReturnsUsers()
        {
            var users = new[]
            {
                new UserProfileDto("test@example.com", "Ben", "Dover"),
                new UserProfileDto("example@test.com", "Mike", "Hunt")
            };

            _mockIdentityUserService.Setup(s => s.GetAllUsersAsync())
                .ReturnsAsync(users);

            var result = await _userService.GetAllUsersAsync();

            Assert.Equal(2, result.Length);
            Assert.Equal("test@example.com", result[0].Email);

        }
    }
}
