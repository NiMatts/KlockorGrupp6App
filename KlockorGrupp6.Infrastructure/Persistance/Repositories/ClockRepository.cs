using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Domain;
using Microsoft.EntityFrameworkCore;

namespace KlockorGrupp6App.Infrastructure.Persistance.Repositories
{
    public class ClockRepository(ApplicationContext context) : IClockRepository
    {
        //List<Clock> clocks = new List<Clock>
        //{
        //    new Clock { Id = 1, Brand = "Rolex", Model = "Submariner", Price = 98000m, Year = new DateTime(2022, 1, 1) },
        //    new Clock { Id = 2, Brand = "Omega", Model = "Speedmaster", Price = 67000m, Year = new DateTime(2021, 1, 1) },
        //    new Clock { Id = 3, Brand = "Tag Heuer", Model = "Carrera", Price = 42000m, Year = new DateTime(2023, 1, 1) },
        //    new Clock { Id = 4, Brand = "Seiko", Model = "Presage", Price = 8900m, Year = new DateTime(2020, 1, 1) },
        //    new Clock { Id = 5, Brand = "Casio", Model = "G-Shock", Price = 1500m, Year = new DateTime(2019, 1, 1) }
        //};
        public async Task AddAsync(Clock clock) =>
            await context.Clocks.AddAsync(clock);

        public void Remove(Clock clock) =>
            context.Clocks.Remove(clock);

        public async Task<Clock[]> GetAllAsync() =>
            await context.Clocks.OrderBy(c => c.Brand).ToArrayAsync();

        public async Task<Clock[]> GetAllByUserIdAsync(string userId) =>
            await context.Clocks.Where(c => c.CreatedByUserID == userId).OrderBy(c => c.Brand).ToArrayAsync();

        public async Task<Clock> GetByIdAsync(int id) =>
            await context.Clocks.SingleAsync(e => e.Id == id);

    }
}
