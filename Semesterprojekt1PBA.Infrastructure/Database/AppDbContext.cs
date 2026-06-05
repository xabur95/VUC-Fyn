using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<School> Schools { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;

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

        modelBuilder.Entity<Class>().OwnsOne(c => c.ClassDateRange);
        modelBuilder.Entity<Class>().OwnsOne(c => c.Title);
        modelBuilder.Entity<School>().OwnsOne(s => s.Title);
        modelBuilder.Entity<Subject>().OwnsOne(s => s.Title);
        modelBuilder.Entity<Question>().OwnsOne(q => q.Title);
        modelBuilder.Entity<Answer>().OwnsOne(a => a.Title);
        modelBuilder.Entity<Tag>().OwnsOne(t => t.Title);
    }
}