using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Users;
using KlockorGrupp6App.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

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
            await ListAllClocksAsync(services);
            await ListAllUserssAsync(services);
        }

        private static async Task ListAllClocksAsync(IServiceProvider services)
        {
            var clockService = services.GetRequiredService<IClockService>();
            var userService = services.GetRequiredService<IIdentityUserService>();

            var clocks = await clockService.GetAllAsync();
            string[,] table = new string[clocks.Length, 5];
            for (int i = 0; i < clocks.Length; i++)
            {
                table[i, 0] = clocks[i].Brand;
                table[i, 1] = clocks[i].Model;
                table[i, 2] = clocks[i].Price.ToString();
                table[i, 3] = clocks[i].Year.ToString();
                table[i, 4] = clocks[i].CreatedByUserID;
            }

            int maxLength = clocks
            .Where(c => c.CreatedByUserID != null) // optional: skip nulls
            .Select(c => c.CreatedByUserID.Length)
            .DefaultIfEmpty(0) // handles empty array case
            .Max();

            int[] columnLeangth = [10, 10, 10, 13, maxLength];
            string[] columnHeads = ["Brand", "Model", "Price", "Release Date", "Created by user"];

            TerminalTable(table, columnLeangth, columnHeads);
        }

        //public string Brand { get; set; } = null!;
        //public string Model { get; set; } = null!;
        //public decimal Price { get; set; }
        //public DateTime Year { get; set; }

        //public string CreatedByUserID { get; set; } = null!;

        //public override string ToString()
        //{
        //    return $"{Brand} {Model} Price: {Price} ";
        //}
        private static async Task ListAllUserssAsync(IServiceProvider services)
        {
            var userService = services.GetRequiredService<IIdentityUserService>();
            var users = await userService.GetAllUsersAsync();
            string[,] table = new string[users.Length, 3];
            for (int i = 0; i < users.Length; i++) {
                table[i, 0] = users[i].Email;
                table[i, 1] = users[i].FirstName;
                table[i, 2] = users[i].LastName;
            }
            int[]columnLeangth = [20, 20, 20];
            string[] columnHeads = ["Username", "Firstname", "Lastname"];

            TerminalTable(table, columnLeangth, columnHeads);
        }

        static public void TerminalTable(string[,] table, int[] columnLengths, string[] columnHeads)
        {

            string tableHead = Rows(columnHeads, columnLengths);
            string dividerRow = "";
            for (int i = 0; i < tableHead.Length; i++) { dividerRow += "-"; }
            Console.WriteLine(tableHead);
            Console.WriteLine(dividerRow);
            int rows = table.GetLength(0);
            int cols = table.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                string[] row = new string[cols];
                for (int j = 0; j < cols; j++)
                {
                    row[j] = table[i, j];
                }
                Console.WriteLine(Rows(row, columnLengths));
            }

            static string Rows(string[] columns, int[] columnLengths)
            {
                string row = string.Empty;
                for (int i = 0; i < columns.Length; i++)
                {
                    if (columns[i].Length > columnLengths[i]) columns[i] = columns[i].Substring(0, columnLengths[i] - 3) + "...";
                    row += columns[i].PadRight(columnLengths[i])+" ";
                }
                return row;
            }
        }
    }
}
