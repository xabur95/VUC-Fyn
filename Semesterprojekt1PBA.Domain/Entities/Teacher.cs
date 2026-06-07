using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

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

    private Teacher(string firstName, string lastName, string email, string password, IReadOnlyCollection<Email> existingEmails)
        : base(firstName, lastName, email, password, RoleType.Teacher, existingEmails)
    { }

    public static Teacher Create(string firstName, string lastName, string email, string password, IReadOnlyCollection<Email> existingEmails)
    {
        var teacher = new Teacher(firstName, lastName, email, password, existingEmails);
        teacher.AssignRole(new UserRole(RoleType.Teacher));
        return teacher;
    }
}