using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Domain;
using Microsoft.EntityFrameworkCore;

namespace KlockorGrupp6App.Infrastructure.Persistance.Repositories
{
    public class ClockRepository(ApplicationContext context) : IClockRepository
    {       
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
