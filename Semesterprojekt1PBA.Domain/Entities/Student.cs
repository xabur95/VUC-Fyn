using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;

public class Student : User
{
    public string Knr { get; private set; } = null!;
    public DateOnly Tilmeldt { get; private set; }
    public DateOnly? Ophørt { get; private set; }

    private Student(string firstName, string lastName, string email, string knr, DateOnly tilmeldt, DateOnly? ophørt)
        : base(firstName, lastName, email, RoleType.Student)
    {
        Knr = knr;
        Tilmeldt = tilmeldt;
        Ophørt = ophørt;
    }

    public static Student Create(string firstName, string lastName, string email, string knr, DateOnly tilmeldt, DateOnly? ophørt)
    {
        var student = new Student(firstName, lastName, email, knr, tilmeldt, ophørt);
        student.AssignRole(new UserRole(RoleType.Student));
        return student;
    }
}