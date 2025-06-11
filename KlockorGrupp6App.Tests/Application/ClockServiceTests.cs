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
        // Mocked dependencies
        private readonly Mock<IClockRepository> _mockClockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        // Service under test
        private readonly ClockService _service;

        public ClockServiceTests()
        {
            // Set up mocks
            _mockClockRepo = new Mock<IClockRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            // Configure UnitOfWork to return the mocked repository
            _mockUnitOfWork.Setup(u => u.Clocks).Returns(_mockClockRepo.Object);

            // Inject mocked UnitOfWork into ClockService
            _service = new ClockService(_mockUnitOfWork.Object);

        }
        #endregion

        [Trait("ClocksService", "GetAll")]
        [Fact]
        public void GetAll_ShouldReturnAllClocks()
        {
            // Arrange: Creating a list with 2 clock-objects
            var clocks = new List<Clock>
            {
                new Clock { Id = 1, Brand = "Rolex", Model = "Submariner", Price = 98000m, Year = new DateTime(2022, 1, 1) },
                new Clock { Id = 2, Brand = "Omega", Model = "Speedmaster", Price = 67000m, Year = new DateTime(2021, 1, 1) }
            };

            // Setting up the mock repository so when GetAll() is called. It will "pretend" to return the clocks list we created above.
            _mockClockRepo.Setup(r => r.GetAll()).Returns(clocks.ToArray());

            // Act: Calling the GetAll method on the service (Wich we have mocked to return the clocks list)
            var result = _service.GetAll();

            // Assert
            Assert.Equal(2, result.Length); // Checking that the result has 2 clocks
            Assert.Equal("Rolex", result[0].Brand); // Checking that the first clock's brand is "Rolex"
        }

        [Trait("ClocksService", "GetAll")]
        [Fact]
        public void GetAll_WhenNoClocksExist_ShouldReturnEmptyArray()
        {
            // Arrange: Configure mock to return an empty array when GetAll is called
            _mockClockRepo.Setup(r => r.GetAll()).Returns(Array.Empty<Clock>());

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Trait("ClocksService", "GetById")]
        [Fact]
        public void GetById_ShouldReturnCorrectClock()
        {
            // Arrange: Creating a clock object with Id 1 and Brand "Rolex"
            var clock = new Clock { Id = 1, Brand = "Rolex" };
            _mockClockRepo.Setup(r => r.GetById(1)).Returns(clock);

            // Act
            var result = _service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Rolex", result.Brand);
        }

        [Trait("ClocksService", "GetById")]
        [Fact]        
        public void GetById_WhenClockDoesNotExist_ShouldReturnNull()
        {
            // Arrange: Simulate no match found
            _mockClockRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns((Clock?)null);

            // Act
            var result = _service.GetById(666);

            // Assert
            Assert.Null(result);
        }

        [Trait("ClocksService", "GetAllByUserId")]
        [Fact]
        public void GetAllByUserId_ShouldReturnOnlyClocksForThatUser()
        {
            // Arrange
            var userId = "user-1";
            var clocks = new[]
            {
            new Clock { Id = 1, Brand = "Rolex" },
            new Clock { Id = 2, Brand = "Omega" }
        };

            _mockClockRepo.Setup(r => r.GetAllByUserId(userId)).Returns(clocks);

            // Act
            var result = _service.GetAllByUserId(userId);

            // Assert
            Assert.Equal(2, result.Length);
        }

        [Trait("ClocksService", "GetAllByUserId")]
        [Fact]
        public void GetAllByUserId_WhenUserHasNoClocks_ShouldReturnEmptyArray()
        {
            // Arrange: Simulating a user with no clocks
            var userId = "user-x";
            _mockClockRepo.Setup(r => r.GetAllByUserId(userId)).Returns(Array.Empty<Clock>());

            // Act
            var result = _service.GetAllByUserId(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Trait("ClocksService", "Add")]
        [Fact]
        public void Add_ShouldCallAddOnRepositoryWithCorrectClock()
        {
            // Arrange
            var clock = new Clock { Id = 1, Brand = "Rolex" };

            // Act
            _service.Add(clock);

            // Assert: Verifying that Add method on the repository is called once with the clock object
            _mockClockRepo.Verify(r => r.Add(clock), Times.Once);
        }

        [Trait("ClocksService", "Add")]
        [Fact]
        public void Add_ShouldCallPersistAllAsync()
        {
            // Arrange
            var clock = new Clock { Id = 1, Brand = "Rolex" };

            // Act
            _service.Add(clock);

            // Assert: Verifying that PersistAllAsync is called once when adding a clock
            _mockUnitOfWork.Verify(u => u.PersistAllAsync(), Times.Once);
        }
    }   
}
