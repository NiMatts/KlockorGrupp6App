using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Clocks.Services;
using KlockorGrupp6App.Infrastructure.Repositories;
namespace KlockorGrupp6App
{
    public class Program
    {
        public static void Main(string[] args)
       {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IClockService, ClockService>();
            builder.Services.AddScoped<IClockRepository, ClockRepository>();
            //builder.Services.AddScoped(IClockService, ClockService);
            //builder.Services.AddScoped(IClockRepository, ClockRepository);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
            
            app.MapControllers();
            app.Run();
        }
    }
}
