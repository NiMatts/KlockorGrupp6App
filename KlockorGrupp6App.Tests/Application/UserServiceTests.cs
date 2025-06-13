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

        [Trait("UserService", "SignOut")]
        [Fact]
        public async Task SignOutAsync_CallsSignOut()
        {

        }
    }
}
