using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Users;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockorGrupp6App.Tests.Web
{
    public class AccountControllerTests
    {
        #region [Test Setup]
        private readonly Mock<IClockService> _mockClockService;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;        
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockClockService = new Mock<IClockService>();

            var mockStore = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(mockStore.Object, null, null, null, null, null, null, null, null);

            _controller = new AccountController(_mockUserService.Object, _mockUserManager.Object, _mockClockService.Object);
        }
        #endregion

    }
}
