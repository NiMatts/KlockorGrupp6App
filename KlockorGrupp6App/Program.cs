using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Clocks.Services;
using KlockorGrupp6App.Application.Users;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped(IClockService, ClockService);
            //builder.Services.AddScoped(IClockRepository, ClockRepository);
            var connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(connString));
            var app = builder.Build();
            
            app.MapControllers();
            app.Run();
        }
    }
}
