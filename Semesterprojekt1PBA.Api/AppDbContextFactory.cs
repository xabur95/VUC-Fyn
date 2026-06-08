using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Semesterprojekt1PBA.Infrastructure.Database;

namespace Semesterprojekt1PBA.Api;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
            ?? "Server=localhost,1433;Database=VucFyn;User Id=sa;Password=ChangeMe123!;TrustServerCertificate=True;";

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connectionString, b => b.MigrationsAssembly("Semesterprojekt1PBA.DatabaseMigration"))
            .Options;

        return new AppDbContext(options);
    }
}
