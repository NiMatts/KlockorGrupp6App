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
using System.Threading.Tasks;


namespace KlockorGrupp6App.Terminal
{
    internal class Program
    {
        static ClockService clockService;
    
        static async Task Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var services = new ServiceCollection();

            // Read connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Add EF Core and Identity
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            // Register your application services
            services.AddScoped<IUserService, Application.Users.IdentityUserService>(); // if IdentityUserService implements IUserService
            services.AddScoped<IIdentityUserService, Infrastructure.Services.IdentityUserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddLogging();
            // Build service provider
            var provider = services.BuildServiceProvider();

            // Resolve a service and use it
            var userService = provider.GetRequiredService<IIdentityUserService>();

            var users = await userService.GetAllUsersAsync();
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Email} - {user.FirstName} {user.LastName}");
            }

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

