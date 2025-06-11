using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlockorGrupp6App.Application;
using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Infrastructure.Persistance.Repositories;


namespace KlockorGrupp6App.Infrastructure.Persistance
{
    public class UnitOfWork(ApplicationContext context, IClockRepository clockRepository) : IUnitOfWork
    {
        public IClockRepository Clocks => clockRepository;

        public async Task PersistAllAsync() => await context.SaveChangesAsync();

    }
}
