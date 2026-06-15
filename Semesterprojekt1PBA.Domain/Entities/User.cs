using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.Policies;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Entities;
/// <summary>
/// Author: Michael
/// Represents an application user with identity, contact information, roles, and activation status.
/// The User class provides methods to assign and revoke roles, update user details, and manage
/// activation state. Role assignment and revocation are subject to policy validation and may throw exceptions if
/// constraints are violated. This class is intended to be used as an aggregate root for user-related operations and
/// enforces business rules around role management.</summary>

public class User : Entity
{
    protected IRolePolicy _rolePolicy = null!;
    private readonly List<UserRole> _roles = [];
    public Name Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

    public bool IsActive { get; private set; } = true;

    protected User()
    {
    }

    protected User(string firstName, string lastName, string email, string password, RoleType roleType, IReadOnlyCollection<Email> existingEmails)
    {
        var newEmail = new Email(email);
        AssureEmailIsUnique(newEmail, existingEmails);

        Name = new Name(firstName, lastName);
        Email = newEmail;
        Password = Password.Create(password);
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

    public void Update(string firstName, string lastName, string email, IReadOnlyCollection<Email> existingEmails)
    {
        var newEmail = new Email(email);

        if (newEmail != Email)
            AssureEmailIsUnique(newEmail, existingEmails);

        Name = new Name(firstName, lastName);
        Email = newEmail;
    }
    
    protected static IRolePolicy CreatePolicy(RoleType roleType)
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

    private static void AssureEmailIsUnique(Email email, IReadOnlyCollection<Email> existingEmails)
    {
        if (existingEmails.Contains(email))
        {
            throw new ErrorException($"Email '{email.Value}' is already in use.", errorCode: "EMAIL_NOT_UNIQUE");
        }
    }
}
