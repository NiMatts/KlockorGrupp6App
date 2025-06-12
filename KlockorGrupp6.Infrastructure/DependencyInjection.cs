using KlockorGrupp6App.Application;
using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Clocks.Services;
using KlockorGrupp6App.Application.Users;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Infrastructure.Persistance.Repositories;
using KlockorGrupp6App.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KlockorGrupp6App.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(connString));

            services.AddScoped<IClockService, ClockService>();
            services.AddScoped<IClockRepository, ClockRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIdentityUserService, IdentityUserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.AccessDeniedPath = "/AccessDenied";
            });

            return services;
        }
    }
}
