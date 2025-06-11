using KlockorGrupp6App.Domain;

namespace KlockorGrupp6App.Application.Clocks.Interfaces;

public interface IClockService
{
    void Add(Clock clock);
    Clock[] GetAll();
    Clock[]? GetAllByUserId(string id);
    Clock? GetById(int id);
}