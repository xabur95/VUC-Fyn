using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Infrastructure.Database;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await SeedUsersAsync(context);
        await SeedTagsAsync(context);
        await SeedSchoolsAsync(context);
        await SeedQuestionsAsync(context);
        await SeedClassStudentsAsync(context);
    }

    private static async Task SeedUsersAsync(AppDbContext context)
    {
        if (await context.Users.AnyAsync())
            return;

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

    private static async Task SeedTagsAsync(AppDbContext context)
    {
        if (await context.Tags.AnyAsync())
            return;

        var tags = new List<Tag>
        {
            Tag.Create("Matematik", "Spørgsmål relateret til matematik"),
            Tag.Create("Dansk", "Spørgsmål relateret til dansk sprog og litteratur"),
            Tag.Create("Engelsk", "Spørgsmål relateret til engelsk sprog"),
            Tag.Create("Programmering", "Spørgsmål om softwareudvikling og kodning"),
            Tag.Create("IT-sikkerhed", "Spørgsmål om cybersikkerhed og netværk"),
        };

        await context.Tags.AddRangeAsync(tags);
        await context.SaveChangesAsync();
    }

    private static async Task SeedSchoolsAsync(AppDbContext context)
    {
        if (await context.Schools.AnyAsync())
            return;

        var school1 = School.Create("Odense VUC", Array.Empty<School>());
        school1.AddClass("HF 1A", new DateOnly(2026, 8, 1), new DateOnly(2027, 6, 30), Array.Empty<Class>());
        school1.AddClass("HF 1B", new DateOnly(2026, 8, 1), new DateOnly(2027, 6, 30), school1.Classes);

        var school2 = School.Create("Svendborg VUC", new[] { school1 });
        school2.AddClass("HF 2A", new DateOnly(2026, 8, 1), new DateOnly(2027, 6, 30), Array.Empty<Class>());

        await context.Schools.AddRangeAsync(school1, school2);
        await context.SaveChangesAsync();
    }

    private static async Task SeedQuestionsAsync(AppDbContext context)
    {
        if (await context.Questions.AnyAsync())
            return;

        var teacher = await context.Users.OfType<Teacher>().FirstOrDefaultAsync();
        if (teacher is null)
            return;

        var tags = await context.Tags.ToListAsync();
        var progTag = tags.FirstOrDefault(t => t.Title.Value == "Programmering");
        var mathTag = tags.FirstOrDefault(t => t.Title.Value == "Matematik");

        var questions = new List<Question>
        {
            Question.Create(teacher, "Hvad er en variabel?",
                "Forklar hvad en variabel er i programmering og giv et eksempel i C#.",
                2, ActiveStatus.Active, null,
                progTag is not null ? new[] { progTag } : null),

            Question.Create(teacher, "Hvad er OOP?",
                "Beskriv de fire grundprincipper i objektorienteret programmering.",
                5, ActiveStatus.Active, null,
                progTag is not null ? new[] { progTag } : null),

            Question.Create(teacher, "Hvad er en løkke?",
                "Forklar forskellen på en for-løkke og en while-løkke og angiv hvornår du bruger hvilken.",
                3, ActiveStatus.Active, null,
                progTag is not null ? new[] { progTag } : null),

            Question.Create(teacher, "Hvad er SOLID?",
                "Beskriv SOLID-principperne og giv et eksempel på ét af dem.",
                5, ActiveStatus.Active, null,
                progTag is not null ? new[] { progTag } : null),

            Question.Create(teacher, "Hvad er 2+2?",
                "Et simpelt regnestykke.",
                1, ActiveStatus.Active, null,
                mathTag is not null ? new[] { mathTag } : null),
        };

        await context.Questions.AddRangeAsync(questions);
        await context.SaveChangesAsync();
    }

    private static async Task SeedClassStudentsAsync(AppDbContext context)
    {
        var anyClassHasStudents = await context.Classes
            .AnyAsync(c => c.Students.Any());

        if (anyClassHasStudents)
            return;

        var classes = await context.Classes
            .Include(c => c.Students)
            .ToListAsync();

        var students = await context.Users.OfType<Student>().ToListAsync();

        if (classes.Count == 0 || students.Count == 0)
            return;

        var hf1a = classes.FirstOrDefault(c => c.Title.Value == "HF 1A");
        var hf1b = classes.FirstOrDefault(c => c.Title.Value == "HF 1B");
        var hf2a = classes.FirstOrDefault(c => c.Title.Value == "HF 2A");

        // STU001–STU010 i HF 1A
        if (hf1a is not null)
            foreach (var s in students.Take(10))
                hf1a.AddStudent(s);

        // STU011–STU020 i HF 1B
        if (hf1b is not null)
            foreach (var s in students.Skip(10).Take(10))
                hf1b.AddStudent(s);

        // De første 5 studerende i HF 2A også
        if (hf2a is not null)
            foreach (var s in students.Take(5))
                hf2a.AddStudent(s);

        await context.SaveChangesAsync();
    }
}
