using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.Policies;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;
/// <summary>
/// Author: Michael
/// Represents an application user with identity, contact information, roles, and activation status.
/// Handles role assignment and removal with validation via role policy.
/// Instances are created via the static Create method, which ensures initial role assignment.
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
    protected User(string firstName, string lastName, string email, RoleType roleType)
    {
        Name = new Name(firstName, lastName);
        Email = new Email(email);
        Id = Guid.NewGuid();
        _rolePolicy = CreatePolicy(roleType);
    }

    public void RevokeRole(UserRole role)
    {
        if (!_roles.Contains(role))
        {
            throw new ErrorException($"User does not have the role, cannot remove:  {role.RoleType}", errorCode: "ROLE_NOT_FOUND");
        }

        if (_roles.Count == 1 && _roles.Contains(role))
        {
            throw new ErrorException($"User only have this single role,cannot remove: {role.RoleType}", errorCode: "ROLE_NOT_FOUND");
        }

        _roles.Remove(role);
    }

    public void AssignRole(UserRole role)
    {
        _rolePolicy.Validate(role.RoleType, Roles);

        if (Roles.Contains(role))
        {
            throw new ErrorException($"User already has the role: {role.RoleType}", errorCode: "ROLE_ALREADY_ASSIGNED");
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

    public void Update(string firstName, string lastName, string email)
    {
        var name = new Name(firstName, lastName);
        Name = name;

        var userEmail = new Email(email);
        Email = userEmail;
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
                throw new ErrorException($"Invalid role type: {roleType}", errorCode: "INVALID_ROLE_TYPE");
        }
    }
 
    public void Deactivate()
    {
        IsActive = false;
    }
}
