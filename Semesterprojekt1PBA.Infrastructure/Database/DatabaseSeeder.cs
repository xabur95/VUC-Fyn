using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

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

        users.Add(Admin.Create("Anders", "Andersen", "anders.andersen@school.dk", "Password1", Array.Empty<Email>()));
        users.Add(Admin.Create("Lise", "Nielsen", "lise.nielsen@school.dk", "Password1", Array.Empty<Email>()));

        users.AddRange(new[]
        {
            Teacher.Create("Donna", "Jensen", "donna.jensen@school.dk", "Password1", Array.Empty<Email>()),
            Teacher.Create("Peter", "Hansen", "peter.hansen@school.dk", "Password1", Array.Empty<Email>()),
            Teacher.Create("Mette", "Larsen", "mette.larsen@school.dk", "Password1", Array.Empty<Email>()),
            Teacher.Create("Thomas", "Sørensen", "thomas.sorensen@school.dk", "Password1", Array.Empty<Email>()),
            Teacher.Create("Camilla", "Madsen", "camilla.madsen@school.dk", "Password1", Array.Empty<Email>()),
            Teacher.Create("Martin", "Pedersen", "martin.pedersen@school.dk", "Password1", Array.Empty<Email>()),
            Teacher.Create("Louise", "Christensen", "louise.christensen@school.dk", "Password1", Array.Empty<Email>()),
            Teacher.Create("Jakob", "Mortensen", "jakob.mortensen@school.dk", "Password1", Array.Empty<Email>()),
            Teacher.Create("Anne", "Knudsen", "anne.knudsen@school.dk", "Password1", Array.Empty<Email>()),
            Teacher.Create("Rasmus", "Poulsen", "rasmus.poulsen@school.dk", "Password1", Array.Empty<Email>())
        });

        users.AddRange(new[]
        {
            Student.Create("Emma", "Jensen", "emma.jensen@stud.school.dk", "Password1", "STU001", new DateOnly(2024, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Noah", "Hansen", "noah.hansen@stud.school.dk", "Password1", "STU002", new DateOnly(2024, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Olivia", "Nielsen", "olivia.nielsen@stud.school.dk", "Password1", "STU003", new DateOnly(2024, 8, 1), null, Array.Empty<Email>()),
            Student.Create("William", "Larsen", "william.larsen@stud.school.dk", "Password1", "STU004", new DateOnly(2024, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Alma", "Madsen", "alma.madsen@stud.school.dk", "Password1", "STU005", new DateOnly(2024, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Lucas", "Pedersen", "lucas.pedersen@stud.school.dk", "Password1", "STU006", new DateOnly(2024, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Freja", "Andersen", "freja.andersen@stud.school.dk", "Password1", "STU007", new DateOnly(2024, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Victor", "Christensen", "victor.christensen@stud.school.dk", "Password1", "STU008", new DateOnly(2024, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Clara", "Poulsen", "clara.poulsen@stud.school.dk", "Password1", "STU009", new DateOnly(2024, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Oscar", "Knudsen", "oscar.knudsen@stud.school.dk", "Password1", "STU010", new DateOnly(2024, 8, 1), null, Array.Empty<Email>()),

            Student.Create("Sofie", "Møller", "sofie.moller@stud.school.dk", "Password1", "STU011", new DateOnly(2025, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Elias", "Kristensen", "elias.kristensen@stud.school.dk", "Password1", "STU012", new DateOnly(2025, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Ida", "Rasmussen", "ida.rasmussen@stud.school.dk", "Password1", "STU013", new DateOnly(2025, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Magnus", "Olsen", "magnus.olsen@stud.school.dk", "Password1", "STU014", new DateOnly(2025, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Laura", "Jakobsen", "laura.jakobsen@stud.school.dk", "Password1", "STU015", new DateOnly(2025, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Anton", "Berg", "anton.berg@stud.school.dk", "Password1", "STU016", new DateOnly(2025, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Anna", "Holm", "anna.holm@stud.school.dk", "Password1", "STU017", new DateOnly(2025, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Malthe", "Friis", "malthe.friis@stud.school.dk", "Password1", "STU018", new DateOnly(2025, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Josefine", "Dahl", "josefine.dahl@stud.school.dk", "Password1", "STU019", new DateOnly(2025, 8, 1), null, Array.Empty<Email>()),
            Student.Create("Sebastian", "Winther", "sebastian.winther@stud.school.dk", "Password1", "STU020", new DateOnly(2025, 8, 1), null, Array.Empty<Email>())
        });

        await context.Users.AddRangeAsync(users);

        await context.SaveChangesAsync();
    }
}