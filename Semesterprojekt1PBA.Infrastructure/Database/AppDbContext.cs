using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Student>("Student")
            .HasValue<Teacher>("Teacher")
            .HasValue<Admin>("Admin");

        modelBuilder.Entity<User>().OwnsOne(u => u.Name);
        modelBuilder.Entity<User>().OwnsOne(u => u.Email);
        modelBuilder.Entity<User>().OwnsMany(u => u.Roles, r => { r.ToTable("UserRoles"); });
    }
}