using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Domain;

namespace KlockorGrupp6App.Application.Clocks.Services;

//public class ClockService(IClockRepository clockRepository) : IClockService
public class ClockService(IUnitOfWork unitOfWork) : IClockService
{
    List<Clock> clocks = new List<Clock>
        {
            new Clock { Id = 1, Brand = "Rolex", Model = "Submariner", Price = 98000m, Year = new DateTime(2022, 1, 1) },
            new Clock { Id = 2, Brand = "Omega", Model = "Speedmaster", Price = 67000m, Year = new DateTime(2021, 1, 1) },
            new Clock { Id = 3, Brand = "Tag Heuer", Model = "Carrera", Price = 42000m, Year = new DateTime(2023, 1, 1) },
            new Clock { Id = 4, Brand = "Seiko", Model = "Presage", Price = 8900m, Year = new DateTime(2020, 1, 1) },
            new Clock { Id = 5, Brand = "Casio", Model = "G-Shock", Price = 1500m, Year = new DateTime(2019, 1, 1) }
        };

    public void Add(Clock clock)
    {
        unitOfWork.Clocks.Add(clock);
        unitOfWork.PersistAllAsync();
    }

    public Clock[] GetAll()
    {
        return unitOfWork.Clocks.GetAll();
    }

    public Clock? GetById(int id)
    {
        return unitOfWork.Clocks.GetById(id);
    }

    //public void Delete(int id)
    //{

    //}
}
