using KlockorGrupp6App.Application;
using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Users;

namespace KlockorGrupp6App.Infrastructure.Persistance
{
    public class UnitOfWork(ApplicationContext context, IClockRepository clockRepository, IIdentityUserService identityUserService) : IUnitOfWork
    {
        public IClockRepository Clocks => clockRepository;
        public IIdentityUserService IdentityUserService => identityUserService;

        public async Task PersistAllAsync() => await context.SaveChangesAsync();

    }
}
