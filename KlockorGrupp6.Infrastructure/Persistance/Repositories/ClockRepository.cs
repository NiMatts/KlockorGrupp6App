using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Domain;

namespace KlockorGrupp6App.Infrastructure.Persistance.Repositories
{
    public class ClockRepository(ApplicationContext context) : IClockRepository
    {
        //List<Clock> clocks = new List<Clock>
        //{
        //    new Clock { Id = 1, Brand = "Rolex", Model = "Submariner", Price = 98000m, Year = new DateTime(2022, 1, 1) },
        //    new Clock { Id = 2, Brand = "Omega", Model = "Speedmaster", Price = 67000m, Year = new DateTime(2021, 1, 1) },
        //    new Clock { Id = 3, Brand = "Tag Heuer", Model = "Carrera", Price = 42000m, Year = new DateTime(2023, 1, 1) },
        //    new Clock { Id = 4, Brand = "Seiko", Model = "Presage", Price = 8900m, Year = new DateTime(2020, 1, 1) },
        //    new Clock { Id = 5, Brand = "Casio", Model = "G-Shock", Price = 1500m, Year = new DateTime(2019, 1, 1) }
        //};
        public void Add(Clock clock)
        {
            
            //context.Clock.Id = clocks.Count < 0 ? 1 : clocks.Max(e => e.Id) + 1;
            context.Clocks.Add(clock);
            context.SaveChanges();
        }


        // Collection expression syntax, introduced in C# 12.
        public Clock[] GetAll() => [.. context.Clocks.OrderBy(c => c.Brand)];

        public Clock[] GetAllByUserId(string userId) => [.. context.Clocks.Where(c => c.CreatedByUserID == userId).OrderBy(c => c.Brand)];
        ////Classic C# syntax for GetAll()
        //public Employee[] GetAll()
        //{
        //    return employees
        //        .OrderBy(e => e.Name)
        //        .ToArray();
        //}

        public Clock GetById(int id) => context.Clocks
            .Single(e => e.Id == id);

        public void Delete(int id)
        {

        }
    }
}
