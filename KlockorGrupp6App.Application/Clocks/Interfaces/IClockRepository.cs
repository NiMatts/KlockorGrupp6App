using KlockorGrupp6App.Domain;

namespace KlockorGrupp6App.Application.Clocks.Interfaces
{
    public interface IClockRepository
    {
        void Add(Clock clock);
        Clock[] GetAll();
        Clock[] GetAllByUserId(string userId);
        Clock GetById(int id);
        void Remove(Clock clock);
    }
}