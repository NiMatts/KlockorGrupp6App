using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlockorGrupp6App.Domain;
using Microsoft.EntityFrameworkCore;

namespace KlockorGrupp6App.Infrastructure.Persistance;

public class ApplicationContext(DbContextOptions<ApplicationContext> options)
    : DbContext(options)
{
    public DbSet<Clock> Clocks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent API - specify database column type for salary
        modelBuilder.Entity<Clock>()
            .Property(s => s.Price)
            .HasColumnType(SqlDbType.Money.ToString());

        // Seed sample data for Employees
        modelBuilder.Entity<Clock>().HasData(
            new Clock { Id = 1, Brand = "Rolex", Model = "Submariner", Price = 98000m, Year = new DateTime(2022, 1, 1) },
            new Clock { Id = 2, Brand = "Omega", Model = "Speedmaster", Price = 67000m, Year = new DateTime(2021, 1, 1) },
            new Clock { Id = 3, Brand = "Tag Heuer", Model = "Carrera", Price = 42000m, Year = new DateTime(2023, 1, 1) },
            new Clock { Id = 4, Brand = "Seiko", Model = "Presage", Price = 8900m, Year = new DateTime(2020, 1, 1) },
            new Clock { Id = 5, Brand = "Casio", Model = "G-Shock", Price = 1500m, Year = new DateTime(2019, 1, 1) }
        );

        // Optional: Seed sample salary data (requires matching foreign key EmployeeId)
        //modelBuilder.Entity<EmployeeSalary>().HasData(
        //    new EmployeeSalary { Id = 1, EmployeeId = 1, Salary = 55000 },
        //    new EmployeeSalary { Id = 2, EmployeeId = 2, Salary = 48000 }
        //);
    }
}
