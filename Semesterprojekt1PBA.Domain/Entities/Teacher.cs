using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;
/// <summary>
/// Author: Michael
/// Repræsenterer en bruger med lærerrollen og funktioner for lærere i systemet.
/// </summary>
/// <remarks>
/// En Teacher kan også have Admin rollen. Brug den statiske Create metode til at oprette en lærer og angive om
/// brugeren også skal have Admin rollen. Kun rollerne lærer og Admin kan tildeles eller fjernes, ellers kastes en
/// exception.
/// </remarks>
public class Teacher : User
{
    protected Teacher()
    {
    }

    private Teacher(string firstName, string lastName, string email)
        : base(firstName, lastName, email)
    {
        AssignRole(new UserRole(RoleType.Teacher));
    }

    public static Teacher Create(string firstName, string lastName, string email, bool isAlsoAdmin = false)
    {
        var teacher = new Teacher(firstName, lastName, email);

        if (isAlsoAdmin)
        {
            teacher.AssignRole(new UserRole(RoleType.Admin));
        }

        return teacher;
    }

    public override void RevokeRole(UserRole role)
    {
        if (role.RoleType == RoleType.Teacher)
        {
            throw new InvalidOperationException("Invalid role type for Teacher. Expected Teacher or Admin.");
        }

        RemoveRole(role);
    }

    public override void AssignRole(UserRole role)
    {
        if (role.RoleType is not RoleType.Teacher and not RoleType.Admin)
        {
            throw new InvalidOperationException("Invalid role type for Teacher. Expected Teacher or Admin.");
        }

        if (Roles.Any(r => r.RoleType == role.RoleType))
        {
            return;
        }

        AddRole(role);
    }

    public bool IsAdmin()
    {
        return Roles.Any(r => r.RoleType == RoleType.Admin);
    }

    public bool IsTeacherOnly()
    {
        return Roles.Any(r => r.RoleType == RoleType.Teacher) &&
               !Roles.Any(r => r.RoleType == RoleType.Admin);
    }
}