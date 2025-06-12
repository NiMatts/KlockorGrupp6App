using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Clocks.Services;
using KlockorGrupp6App.Application.Users;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Infrastructure.Persistance.Repositories;
using KlockorGrupp6App.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KlockorGrupp6App.Application;

namespace KlockorGrupp6App.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IClockService, ClockService>();
            builder.Services.AddScoped<IClockRepository, ClockRepository>();
            builder.Services.AddScoped<IUserService, Application.Users.IdentityUserService>();
            builder.Services.AddScoped<IIdentityUserService, Infrastructure.Services.IdentityUserService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/AccessDenied";
            });

            //builder.Services.AddScoped(IClockService, ClockService);
            //builder.Services.AddScoped(IClockRepository, ClockRepository);
            var connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(connString));
            var app = builder.Build();
            
            //app.UseAuthentication(); //test morgon
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
