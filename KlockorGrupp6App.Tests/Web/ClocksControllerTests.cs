using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Domain;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Tests.Helpers;
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
    }
}
