using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

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

    private Admin(string firstName, string lastName, string email, string password, IReadOnlyCollection<Email> existingEmails)
        : base(firstName, lastName, email, password, RoleType.Admin, existingEmails)
    { }

    public static Admin Create(string firstName, string lastName, string email, string password, IReadOnlyCollection<Email> existingEmails)
    {
        var admin = new Admin(firstName, lastName, email, password, existingEmails);
        admin.AssignRole(new UserRole(RoleType.Admin));
        return admin;
    }
}