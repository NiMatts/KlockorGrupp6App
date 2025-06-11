using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Clocks.Services;
using KlockorGrupp6App.Domain;
using KlockorGrupp6App.Application;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockorGrupp6App.Tests.Application
{
    public class ClockServiceTests
    {
        #region [Test Setup]
        private readonly Mock<IClockRepository> _mockClockRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork; // UnitOfWork still needs to be implemented        
        private readonly ClockService _service;

        public ClockServiceTests()
        {
            _mockClockRepository = new Mock<IClockRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.Clocks).Returns(_mockClockRepository.Object);
            _service = new ClockService(_mockUnitOfWork.Object);

        }
        #endregion

        [Fact]
        public void GetAll_ShouldReturnAllClocks()
        {
            // Arrange - Creating a list with 2 clock-objects
            var clocks = new List<Clock>
            {
                new Clock { Id = 1, Brand = "Rolex", Model = "Submariner", Price = 98000m, Year = new DateTime(2022, 1, 1) },
                new Clock { Id = 2, Brand = "Omega", Model = "Speedmaster", Price = 67000m, Year = new DateTime(2021, 1, 1) }
            };

            // Setting up the mock repository so when GetAll() is called. It will "pretend" to return the clocks list we created above.
            _mockClockRepository.Setup(repo => repo.GetAll()).Returns(clocks.ToArray());

            // Act - Calling the GetAll method on the service (Wich we have mocked to return the clocks list)
            var result = _service.GetAll();

            // Assert
            Assert.Equal(2, result.Length); // Checking that the result has 2 clocks
            Assert.Equal("Rolex", result[0].Brand); // Checking that the first clock's brand is "Rolex"
        }

        [Fact]
        public void GetAll_WhenNoClocksExist_ShouldReturnEmptyArray()
        {
            // Arrange
            _mockClockRepository.Setup(repo => repo.GetAll()).Returns(Array.Empty<Clock>());

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetById_WhenClockDoesNotExist_ShouldReturnNull()
        {
            _mockClockRepository.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Clock?)null);

            var result = _service.GetById(666);

            Assert.Null(result);
        }

        //Still a work in progress. Need to fix Async methods and implement UnitOfWork before getting things in order.
    }
}