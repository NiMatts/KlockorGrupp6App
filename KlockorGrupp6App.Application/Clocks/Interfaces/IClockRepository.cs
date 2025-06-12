using KlockorGrupp6App.Domain;

namespace KlockorGrupp6App.Application.Clocks.Interfaces
{
    public interface IClockRepository
    {
        Task AddAsync(Clock clock);
        Task<Clock[]> GetAllAsync();
        Task<Clock[]> GetAllByUserIdAsync(string userId);
        Task<Clock> GetByIdAsync(int id);
        void Remove(Clock clock);
    }
}