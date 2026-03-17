using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;

public class Student : User
{
    protected Student()
    {
    }

    private Student(string firstName, string lastName, string email) : base(firstName, lastName, email)
    {
        AssignRole(new UserRole(RoleType.Student));
    }

    public static Student Create(string firstName, string lastName, string email)
    {
        return new Student(firstName, lastName, email);
    }

    public override void AssignRole(UserRole role)
    {
        if (role.RoleType != RoleType.Student)
        {
            throw new InvalidOperationException("Invalid role type for Student.");
        }

        if (Roles.Any(r => r.RoleType == role.RoleType))
        {
            return;
        }
        AddRole(role);
    }
}