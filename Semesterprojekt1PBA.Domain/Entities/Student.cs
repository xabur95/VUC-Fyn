namespace Semesterprojekt1PBA.Domain.Entities;

public class Student : User
{
    protected Student()
    {
    }

    public Student(string firstName, string lastName, string email) : base(firstName, lastName, email)
    {
    }

    public static Student Create(string firstName, string lastName, string email)
    {
        return new Student(firstName, lastName, email);
    }
}