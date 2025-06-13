using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Users;

namespace KlockorGrupp6App.Application
{
    public interface IUnitOfWork
    {
        IClockRepository Clocks { get; }
        IIdentityUserService IdentityUserService { get; }

        Task PersistAllAsync();
    }
}