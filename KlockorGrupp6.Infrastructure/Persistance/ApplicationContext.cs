using KlockorGrupp6App.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KlockorGrupp6App.Infrastructure.Persistance;

public class ApplicationContext(DbContextOptions<ApplicationContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
{
    public DbSet<Clock> Clocks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent API - specify database column type for salary
        modelBuilder.Entity<Clock>()
            .Property(s => s.Price)
            .HasColumnType("money");

        modelBuilder.Entity<Clock>()
        .HasOne<ApplicationUser>()  // No direct navigation in domain model
        .WithMany(u => u.Clocks)
        .HasForeignKey(p => p.CreatedByUserID)
        .IsRequired();
                
    }
}
