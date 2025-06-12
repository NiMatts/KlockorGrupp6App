

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
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;


namespace KlockorGrupp6App.Terminal
{
    internal class Program
    {
        static ClockService clockService;
        static UserService userService;
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

            //UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>();
            //SignInManager<ApplicationUser> signInManager = new();
            //RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>;


            //IdentityUserService identityUserService = new(userManager, signInManager, roleManager);
            //userService = new(identityUserService);
            await ListAllClocksAsync();
        }
        private static async Task ListAllClocksAsync()
        {
            foreach (var clock in await clockService.GetAllAsync())
                Console.WriteLine($"{clock.ToSring()}");

            Console.WriteLine("----------------------------");
        }
    }
}

//using KlockorGrupp6App.Application;
//using KlockorGrupp6App.Application.Clocks.Interfaces;
//using KlockorGrupp6App.Application.Clocks.Services;
//using KlockorGrupp6App.Application.Users;
//using KlockorGrupp6App.Infrastructure.Persistance;
//using KlockorGrupp6App.Infrastructure.Persistance.Repositories;
//using KlockorGrupp6App.Infrastructure.Services;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//namespace KlockorGrupp6App.Terminal;

//internal class Program
//{
//    static async Task Main(string[] args)
//    {
//        var host = Host.CreateDefaultBuilder(args)
//            .ConfigureAppConfiguration((context, config) =>
//            {
//                config.AddJsonFile("appsettings.json", optional: false);
//            })
//            .ConfigureServices((context, services) =>
//            {
//                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

//                // Identity + EF Core
//                services.AddDbContext<ApplicationContext>(options =>
//                    options.UseSqlServer(connectionString));

//                services.AddIdentity<ApplicationUser, IdentityRole>()
//                        .AddEntityFrameworkStores<ApplicationContext>()
//                        .AddDefaultTokenProviders();

//                // Repositories + Services
//                services.AddScoped<IClockRepository, ClockRepository>();
//                services.AddScoped<IUnitOfWork, UnitOfWork>();
//                services.AddScoped<IClockService, ClockService>();

//                services.AddScoped<IIdentityUserService, IdentityUserService>();
//                services.AddScoped<UserService>(); // Your own abstraction layer on top of Identity
//            })
//            .Build();

//        // Resolve services using DI
//        using var scope = host.Services.CreateScope();
//        var clockService = scope.ServiceProvider.GetRequiredService<IClockService>();
//        var userService = scope.ServiceProvider.GetRequiredService<UserService>();

//        await ListAllClocksAsync(clockService);
//    }

//    private static async Task ListAllClocksAsync(IClockService clockService)
//    {
//        var clocks = await clockService.GetAllAsync();

//        foreach (var clock in clocks)
//            Console.WriteLine(clock.ToString());

//        Console.WriteLine("----------------------------");
//    }


//}