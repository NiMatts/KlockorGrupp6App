using KlockorGrupp6App.Domain;

namespace KlockorGrupp6App.Application.Clocks.Interfaces
{
    public interface IClockRepository
    {
        void Add(Clock clock);
        void Delete(int id);
        Clock[] GetAll();
        Clock GetById(int id);
    }
}