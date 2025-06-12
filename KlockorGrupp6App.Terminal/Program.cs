using KlockorGrupp6App.Application;
using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Clocks.Services;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;


namespace KlockorGrupp6App.Terminal
{
    internal class Program
    {
        static ClockService clockService;
        static async Task Main(string[] args)
        {
            string connectionString;

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", false);
            var app = builder.Build();
            connectionString = app.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(connectionString)
                .Options;


            var context = new ApplicationContext(options);
            IClockRepository clockRepository = new ClockRepository(context);
            IUnitOfWork unitOfWork = new UnitOfWork(context, clockRepository);
            clockService = new(unitOfWork);

            await ListAllClocksAsync();
        }
        private static async Task ListAllClocksAsync()
        {
            foreach (var item in await clockService.GetAllAsync())
                Console.WriteLine($"{item.Brand}");

            Console.WriteLine("----------------------------");
        }
    }
}

