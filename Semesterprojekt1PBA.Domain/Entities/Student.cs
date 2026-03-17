using Semesterprojekt1PBA.Domain.ValueObjects;

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

    public override void AssignRole(UserRole role)
    {
        if (role.RoleType != RoleType.Student)
        {
            throw new Exception("Invalid role type for Student. Expected RoleType.Student.");
        }
        AddRole(role);
    }

   
}