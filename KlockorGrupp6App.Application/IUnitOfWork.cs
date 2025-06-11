using KlockorGrupp6App.Application.Clocks.Interfaces;

namespace KlockorGrupp6App.Application
{
    public interface IUnitOfWork
    {
        IClockRepository Clocks { get; }

        Task PersistAllAsync();
    }
}