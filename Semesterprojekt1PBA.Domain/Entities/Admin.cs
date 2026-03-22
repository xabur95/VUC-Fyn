using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;
/// <summary>
/// Author: Michael
/// Repræsenterer en bruger med Admin rollen.
/// </summary>
/// <remarks>
/// Admin sikrer, at kun Admin rollen kan tildeles. Andre roller kan ikke tildeles eller fjernes og vil give en
/// undtagelse. Brug den statiske Create metode til at oprette en Admin.
/// </remarks>
public class Admin : User
{
    protected Admin()
    {
    }

    private Admin(string firstName, string lastName, string email)
        : base(firstName, lastName, email)
    {
        AssignRole(new UserRole(RoleType.Admin));
    }

    public static Admin Create(string firstName, string lastName, string email)
    {
        return new Admin(firstName, lastName, email);
    }

    public override void RevokeRole(UserRole role)
    {
        if (role.RoleType == RoleType.Admin)
        {
            throw new InvalidOperationException("Cannot revoke Admin role from an Admin user.");
        }
        RemoveRole(role);
    }

    public override void AssignRole(UserRole role)
    {
        if (role.RoleType != RoleType.Admin)
        {
            throw new InvalidOperationException("Invalid role type for Admin. Expected RoleType.Admin.");
        }

        if (Roles.Any(r => r.RoleType == role.RoleType))
        {
            return;
        }

        AddRole(role);
    }
}