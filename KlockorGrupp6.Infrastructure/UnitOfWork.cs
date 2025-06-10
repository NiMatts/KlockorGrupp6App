using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Infrastructure.Persistance;

namespace KlockorGrupp6App.Infrastructure
{
    public class UnitOfWork(
        ApplicationContext context,
        IClockRepository clockRepository
        )
    {
        //public IClockRepository
    }
}
