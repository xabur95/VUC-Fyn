using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Infrastructure.Database;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Users.AnyAsync())
        {
            return;
        }

        var users = new List<User>();

        users.Add(Admin.Create("Anders", "Andersen", "anders.andersen@school.dk"));
        users.Add(Admin.Create("Lise", "Nielsen", "lise.nielsen@school.dk"));

        users.AddRange([
            Teacher.Create("Donna", "Jensen", "donna.jensen@school.dk"),
            Teacher.Create("Peter", "Hansen", "peter.hansen@school.dk"),
            Teacher.Create("Mette", "Larsen", "mette.larsen@school.dk"),
            Teacher.Create("Thomas", "Sørensen", "thomas.sorensen@school.dk"),
            Teacher.Create("Camilla", "Madsen", "camilla.madsen@school.dk"),
            Teacher.Create("Martin", "Pedersen", "martin.pedersen@school.dk"),
            Teacher.Create("Louise", "Christensen", "louise.christensen@school.dk"),
            Teacher.Create("Jakob", "Mortensen", "jakob.mortensen@school.dk"),
            Teacher.Create("Anne", "Knudsen", "anne.knudsen@school.dk"),
            Teacher.Create("Rasmus", "Poulsen", "rasmus.poulsen@school.dk")
        ]);

        users.AddRange([
            Student.Create("Emma", "Jensen", "emma.jensen@stud.school.dk", "STU001", new DateOnly(2024, 8, 1), null),
            Student.Create("Noah", "Hansen", "noah.hansen@stud.school.dk", "STU002", new DateOnly(2024, 8, 1), null),
            Student.Create("Olivia", "Nielsen", "olivia.nielsen@stud.school.dk", "STU003", new DateOnly(2024, 8, 1), null),
            Student.Create("William", "Larsen", "william.larsen@stud.school.dk", "STU004", new DateOnly(2024, 8, 1), null),
            Student.Create("Alma", "Madsen", "alma.madsen@stud.school.dk", "STU005", new DateOnly(2024, 8, 1), null),
            Student.Create("Lucas", "Pedersen", "lucas.pedersen@stud.school.dk", "STU006", new DateOnly(2024, 8, 1), null),
            Student.Create("Freja", "Andersen", "freja.andersen@stud.school.dk", "STU007", new DateOnly(2024, 8, 1), null),
            Student.Create("Victor", "Christensen", "victor.christensen@stud.school.dk", "STU008", new DateOnly(2024, 8, 1), null),
            Student.Create("Clara", "Poulsen", "clara.poulsen@stud.school.dk", "STU009", new DateOnly(2024, 8, 1), null),
            Student.Create("Oscar", "Knudsen", "oscar.knudsen@stud.school.dk", "STU010", new DateOnly(2024, 8, 1), null),

            Student.Create("Sofie", "Møller", "sofie.moller@stud.school.dk", "STU011", new DateOnly(2025, 8, 1), null),
            Student.Create("Elias", "Kristensen", "elias.kristensen@stud.school.dk", "STU012", new DateOnly(2025, 8, 1), null),
            Student.Create("Ida", "Rasmussen", "ida.rasmussen@stud.school.dk", "STU013", new DateOnly(2025, 8, 1), null),
            Student.Create("Magnus", "Olsen", "magnus.olsen@stud.school.dk", "STU014", new DateOnly(2025, 8, 1), null),
            Student.Create("Laura", "Jakobsen", "laura.jakobsen@stud.school.dk", "STU015", new DateOnly(2025, 8, 1), null),
            Student.Create("Anton", "Berg", "anton.berg@stud.school.dk", "STU016", new DateOnly(2025, 8, 1), null),
            Student.Create("Anna", "Holm", "anna.holm@stud.school.dk", "STU017", new DateOnly(2025, 8, 1), null),
            Student.Create("Malthe", "Friis", "malthe.friis@stud.school.dk", "STU018", new DateOnly(2025, 8, 1), null),
            Student.Create("Josefine", "Dahl", "josefine.dahl@stud.school.dk", "STU019", new DateOnly(2025, 8, 1), null),
            Student.Create("Sebastian", "Winther", "sebastian.winther@stud.school.dk", "STU020", new DateOnly(2025, 8, 1), null)
        ]);

        await context.Users.AddRangeAsync(users);

        await context.SaveChangesAsync();
    }
}