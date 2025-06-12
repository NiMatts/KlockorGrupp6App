using KlockorGrupp6App.Domain;
using Moq;
using KlockorGrupp6App.Tests.Helpers;

namespace KlockorGrupp6App.Tests.Application
{
    public class ClockServiceTests
    {

        [Trait("ClocksService", "GetAll")]
        [Fact]
        public async Task GetAll_ShouldReturnAllClocks()
        {

            var service = TestHelper.CreateClockServiceWithMocks(out var mockClockRepo, out var mockUnit);

            var clocks = new[]
            {
                new Clock { Id = 1, Brand = "Rolex", Model = "Submariner", Price = 98000m, Year = new DateTime(2022, 1, 1) },
                new Clock { Id = 2, Brand = "Omega", Model = "Speedmaster", Price = 67000m, Year = new DateTime(2021, 1, 1) }
            };

            mockClockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(clocks);

            var result = await service.GetAllAsync();

            Assert.Equal(2, result.Length);
            Assert.Equal("Rolex", result[0].Brand);
        }

        [Trait("ClocksService", "GetAll")]
        [Fact]
        public async Task GetAll_WhenNoClocksExist_ShouldReturnEmptyArray()
        {
            var service = TestHelper.CreateClockServiceWithMocks(out var mockClockRepo, out _);
            mockClockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync([]);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Trait("ClocksService", "GetById")]
        [Fact]
        public async Task GetById_ShouldReturnCorrectClock()
        {
            var service = TestHelper.CreateClockServiceWithMocks(out var mockClockRepo, out _);
            var clock = new Clock { Id = 1, Brand = "Rolex" };
            mockClockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(clock);

            var result = await service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Rolex", result.Brand);
        }

        [Trait("ClocksService", "GetById")]
        [Fact]
        public async Task GetById_WhenClockDoesNotExist_ShouldReturnNull()
        {
            var service = TestHelper.CreateClockServiceWithMocks(out var mockClockRepo, out _);
            mockClockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Clock?)null);

            var result = await service.GetByIdAsync(666);

            Assert.Null(result);
        }

        [Trait("ClocksService", "GetAllByUserId")]
        [Fact]
        public async Task GetAllByUserId_ShouldReturnOnlyClocksForThatUser()
        {
            var service = TestHelper.CreateClockServiceWithMocks(out var mockClockRepo, out _);
            var userId = "user-1";
            var clocks = new[]
            {
                new Clock { Id = 1, Brand = "Rolex" },
                new Clock { Id = 2, Brand = "Omega" }
            };

            mockClockRepo.Setup(r => r.GetAllByUserIdAsync(userId)).ReturnsAsync(clocks);

            var result = await service.GetAllByUserIdAsync(userId);

            Assert.Equal(2, result!.Length);
        }

        [Trait("ClocksService", "GetAllByUserId")]
        [Fact]
        public async Task GetAllByUserId_WhenUserHasNoClocks_ShouldReturnEmptyArray()
        {
            var service = TestHelper.CreateClockServiceWithMocks(out var mockClockRepo, out _);
            var userId = "user-x";
            mockClockRepo.Setup(r => r.GetAllByUserIdAsync(userId)).ReturnsAsync([]);

            var result = await service.GetAllByUserIdAsync(userId);

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Trait("ClocksService", "Add")]
        [Fact]
        public async Task Add_ShouldCallAddOnRepositoryWithCorrectClock()
        {
            var service = TestHelper.CreateClockServiceWithMocks(out var mockClockRepo, out _);
            var clock = new Clock { Id = 1, Brand = "Rolex" };

            await service.AddAsync(clock);

            mockClockRepo.Verify(r => r.AddAsync(clock), Times.Once);
        }

        [Trait("ClocksService", "Add")]
        [Fact]
        public async Task Add_ShouldCallPersistAllAsync()
        {
            var service = TestHelper.CreateClockServiceWithMocks(out _, out var mockUnitOfWork);
            var clock = new Clock { Id = 1, Brand = "Rolex" };

            await service.AddAsync(clock);

            mockUnitOfWork.Verify(u => u.PersistAllAsync(), Times.Once);
        }

        [Trait("ClocksService", "Add")]
        [Fact]
        public async Task Add_WhenPersistFails_ShouldThrowAsync()
        {
            var service = TestHelper.CreateClockServiceWithMocks(out _, out var mockUnitOfWork);
            var clock = new Clock { Id = 1, Brand = "Rolex" };
            mockUnitOfWork.Setup(u => u.PersistAllAsync()).Throws(new Exception("Database error"));

            var ex = await Assert.ThrowsAsync<Exception>(() => service.AddAsync(clock));

            Assert.Equal("Database error", ex.Message);
        }

    }
}
