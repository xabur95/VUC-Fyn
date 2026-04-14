using Semesterprojekt1PBA.Domain.ValueObjects;

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

    private Student(string firstName, string lastName, string email, string knr, DateOnly startDate, DateOnly? endDate)
        : base(firstName, lastName, email, RoleType.Student)
    {
        Knr = knr;
        StartDate = startDate;
        EndDate = endDate;
    }

    public static Student Create(string firstName, string lastName, string email, string knr, DateOnly tilmeldt, DateOnly? ophørt)
    {
        var student = new Student(firstName, lastName, email, knr, tilmeldt, ophørt);
        student.AssignRole(new UserRole(RoleType.Student));
        return student;
    }
}