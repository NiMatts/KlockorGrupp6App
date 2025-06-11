using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Domain;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Web.Controllers;
using KlockorGrupp6App.Web.Views.Klockor;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockorGrupp6App.Tests.WebTests
{
    public class ClocksControllerTests
    {
        #region [Test Setup]
        private readonly Mock<IClockService> _mockClockService;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly ClocksController _controller;

        public ClocksControllerTests()
        {
            _mockClockService = new Mock<IClockService>();
            var mockStore = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(mockStore.Object, null, null, null, null, null, null, null, null);

            _controller = new ClocksController(_mockClockService.Object, _mockUserManager.Object);
        }
        #endregion

        [Trait("ClocksController", "Index")]
        [Fact]
        public void Index_ShouldReturnViewWithCorrectData()
        {
            // Arrange: Creating a list with 2 clock-objects
            var clocks = new[]
            {
                new Clock { Id = 1, Brand = "Rolex", Model = "Submariner" },
                new Clock { Id = 2, Brand = "Omega", Model = "Speedmaster" }
            };

            // Setting up the mock service so when GetAll() is called, it will "pretend" to return the clocks list we created above.
            _mockClockService.Setup(s => s.GetAll()).Returns(clocks);

            // Act: Calling the Index method on the controller
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result); // Checking that the result is not null
            var model = Assert.IsType<IndexVM>(result.Model); // Checking that the model is of type IndexVM
            Assert.Equal(2, model.ClocksItems.Length); // Checking that the model contains 2 clock items
            Assert.Equal("Rolex", model.ClocksItems[0].Brand); // Checking that the first clock's brand is "Rolex"
        }
    }
}
