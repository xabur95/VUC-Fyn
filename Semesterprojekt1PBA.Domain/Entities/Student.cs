using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;
/// <summary>
/// Author: Michael
/// Repræsenterer en bruger med student rollen og funktioner for studerende.
/// </summary>
/// <remarks>
/// Student klassen sikrer, at kun student rollen kan tildeles eller fjernes. Brug den statiske Create metode til
/// at oprette en Student. Klassen nedarver fra User og bruges ved rollebaseret brugerhåndtering.
/// </remarks>
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

    public override void RevokeRole(UserRole role)
    {
        if (role.RoleType == RoleType.Student)
        {
            throw new InvalidOperationException("Invalid role type for Student.");
        }
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