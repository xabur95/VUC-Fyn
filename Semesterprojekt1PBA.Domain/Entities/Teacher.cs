using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;
/// <summary>
/// Author: Michael
/// Represents a user with the Teacher role, providing functionality specific to teachers within the system.
/// Use this class to create and manage users who are assigned the Teacher role. Inherits from the User
/// class and applies role-based policies relevant to teachers.</summary>
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