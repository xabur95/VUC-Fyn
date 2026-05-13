using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;
/// <summary>
/// Author: Michael
/// Represents a user with administrative privileges and capabilities.
/// The Admin class provides functionality specific to users who require elevated permissions within the
/// system. Instances of this class are intended to be created using the static Create method to ensure proper role
/// assignment.</summary>
public class Admin : User
{
    protected Admin()
    {
        _rolePolicy = CreatePolicy(RoleType.Admin);
    }

    protected Admin(string firstName, string lastName, string email) : base(firstName, lastName, email, RoleType.Admin) { }

    public static Admin Create(string firstName, string lastName, string email)
    {
        var admin = new Admin(firstName, lastName, email);
        admin.AssignRole(new UserRole(RoleType.Admin));
        return admin;
    }
}