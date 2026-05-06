using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;

public class Teacher : User
{
    protected Teacher()
    {
        _rolePolicy = CreatePolicy(RoleType.Teacher);
    }

    protected Teacher(string firstName, string lastName, string email) :base(firstName, lastName, email, RoleType.Teacher) 
    { }

    public static Teacher Create(string firstName, string lastName, string email)
    {
        var teacher = new Teacher(firstName, lastName, email);
        teacher.AssignRole(new UserRole(RoleType.Teacher));
        return teacher;
    }
}