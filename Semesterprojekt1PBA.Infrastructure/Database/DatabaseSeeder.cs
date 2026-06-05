using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Infrastructure.Database;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.Users.Any()) return;

        var admin = Admin.Create("Anders", "And", "anders@and.dk");
        var teacher = Teacher.Create("Donna", "And", "donna@and.dk");
        var student = Student.Create("Donald", "And", "donald@and.dk", "STU001", new DateOnly(2025, 8, 1), null);

        await context.Users.AddAsync(admin);
        await context.Users.AddAsync(teacher);
        await context.Users.AddAsync(student);

        await context.SaveChangesAsync();
    }
}
