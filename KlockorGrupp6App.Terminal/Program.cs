using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Users;
using KlockorGrupp6App.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KlockorGrupp6App.Terminal
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddInfrastructure(context.Configuration);
                })
                .Build();

            await RunAsync(host.Services);
        }

        private static async Task RunAsync(IServiceProvider services)
        {
            await GetAllClocksAsync(services);
            await GetAllUserssAsync(services);
        }

        private static async Task GetAllClocksAsync(IServiceProvider services)
        {
            var clockService = services.GetRequiredService<IClockService>();

            var clocks = await clockService.GetAllAsync();

            foreach (var clock in clocks)
            {
                Console.WriteLine(clock.ToString());
            }
        }
        private static async Task GetAllUserssAsync(IServiceProvider services)
        {
            var userService = services.GetRequiredService<IIdentityUserService>();
            var users = await userService.GetAllUsersAsync();

            foreach (var user in users)
            {
                Console.WriteLine(user.ToString());
            }
        }

    }
}
