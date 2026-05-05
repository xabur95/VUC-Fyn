using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Entities;

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