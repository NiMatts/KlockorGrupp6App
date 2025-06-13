using KlockorGrupp6App.Application;
using KlockorGrupp6App.Application.Clocks.Interfaces;

namespace KlockorGrupp6App.Infrastructure.Persistance
{
    public class UnitOfWork(ApplicationContext context, IClockRepository clockRepository) : IUnitOfWork
    {
        public IClockRepository Clocks => clockRepository;

        public async Task PersistAllAsync() => await context.SaveChangesAsync();

    }
}
