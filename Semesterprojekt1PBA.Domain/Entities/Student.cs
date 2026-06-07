using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Entities;
/// <summary>
/// Author: Michael
/// Represents a student user with enrollment date and student number.
/// Instantiate via the static Create method.
/// </summary>
public class Student : User
{
    public string Knr { get; private set; } = null!;
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }

    protected Student()
    {
        _rolePolicy = CreatePolicy(RoleType.Student);
    }

    private Student(string firstName, string lastName, string email, string password, string knr, DateOnly startDate, DateOnly? endDate, IReadOnlyCollection<Email> existingEmails)
        : base(firstName, lastName, email, password, RoleType.Student, existingEmails)
    {
        Knr = knr;
        StartDate = startDate;
        EndDate = endDate;
    }

    public static Student Create(string firstName, string lastName, string email, string password, string knr, DateOnly tilmeldt, DateOnly? ophørt, IReadOnlyCollection<Email> existingEmails)
    {
        var student = new Student(firstName, lastName, email, password, knr, tilmeldt, ophørt, existingEmails);
        student.AssignRole(new UserRole(RoleType.Student));
        return student;
    }
}