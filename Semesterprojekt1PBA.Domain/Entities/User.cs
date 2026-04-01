using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.Policies;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;
/// <summary>
/// Author: Michael
/// Repræsenterer en applikationsbruger med identitet, kontaktinformation, roller og aktiveringsstatus.
/// Håndterer rolletildeling og -fjernelse med validering via role policy. 
/// Instanser oprettes via static Create metode som sikrer initial rolletildeling.
/// </summary>
public class User : Entity
{
    private readonly IRolePolicy _rolePolicy = null!;
    private readonly List<UserRole> _roles = [];
    public Name Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

    public bool IsActive { get; private set; } = true;

    protected User()
    {
    }

    private User(string firstName, string lastName, string email, IRolePolicy rolePolicy)
    {
        var name = new Name(firstName, lastName);
        Name = name;

        var userEmail = new Email(email);
        Email = userEmail;

        Id = Guid.NewGuid();

        _rolePolicy = rolePolicy;
    }

    public void RevokeRole(UserRole role)
    {
        if (!_roles.Contains(role))
        {
            throw new InvalidOperationException($"User does not have the role, cannot remove: {role.RoleType}");
        }

        if (_roles.Count == 1 && _roles.Contains(role))
        {
            throw new InvalidOperationException($"User only have this single role, cannot remove: {role.RoleType}");
        }

        _roles.Remove(role);
    }

    public void AssignRole(UserRole role)
    {
        _rolePolicy.Validate(role.RoleType, Roles);

        if (Roles.Contains(role))
        {
            throw new InvalidOperationException($"User already has the role: {role.RoleType}");
        }

        _roles.Add(role);
    }

    public static User Create(string firstName, string lastName, string email, RoleType roleType)
    {
        var policy = CreatePolicy(roleType);

        var user = new User(firstName, lastName, email, policy);

        user.AssignRole(new UserRole(roleType));

        return user;
    }

    private static IRolePolicy CreatePolicy(RoleType roleType)
    {
        switch (roleType)
        {
            case RoleType.Student:
                return new RolePolicies.StudentRolePolicy();
            case RoleType.Teacher:
                return new RolePolicies.TeacherRolePolicy();
            case RoleType.Admin:
                return new RolePolicies.AdminRolePolicy();
            default:
                throw new ArgumentException($"Invalid role type: {roleType}");
        }
    }

    public void Update(string firstName, string lastName, string email)
    {
        var name = new Name(firstName, lastName);
        Name = name;

        var userEmail = new Email(email);
        Email = userEmail;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
