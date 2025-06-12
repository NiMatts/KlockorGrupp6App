using KlockorGrupp6App.Domain;

namespace KlockorGrupp6App.Application.Clocks.Interfaces
{
    public interface IClockService
    {
        Task AddAsync(Clock clock);
        Task<Clock[]> GetAllAsync();
        Task<Clock[]?> GetAllByUserId(string id);
        Task<Clock?> GetByIdAsync(int id);
        Task RemoveAsync(Clock clock);
    }
}