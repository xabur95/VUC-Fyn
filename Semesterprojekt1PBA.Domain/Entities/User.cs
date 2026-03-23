using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.Policies;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;
/// <summary>
/// Author: Michael
/// Represents an application user with identity, contact information, and assigned roles.
/// </summary>
/// <remarks>The User class provides methods for assigning and revoking roles, as well as static factory methods
/// for creating users with specific role configurations such as student, teacher, school administrator, or
/// administrator. Role assignment is validated according to the applicable role policy. The class exposes read-only
/// access to the user's roles and supports updating the user's name and email address.</remarks>
public class User : Entity
{
    public Name Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;

    private readonly List<UserRole> _roles = [];
    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();
    
    private readonly IRolePolicy _roleService;

    protected User()
    {
    }

    private User(string firstName, string lastName, string email, IRolePolicy roleService)
    {
        var name = new Name(firstName, lastName);
        Name = name;

        var userEmail = new Email(email);
        Email = userEmail;

        Id = Guid.NewGuid();

        _roleService = roleService;
    }
  
    public void RevokeRole(UserRole role)
    {
        _roles.Remove(role);
    }

    public void AssignRole(UserRole role)
    {
        _roleService.Validate(role.RoleType, Roles);

        if (Roles.Contains(role))
        {
            throw new InvalidOperationException($"User already has the role: {role.RoleType}");
        }

        _roles.Add(role);
    }

    public static User CreateStudent(string firstName, string lastName, string email)
    {
        var user = new User(firstName, lastName, email, new RolePolicies.StudentRolePolicy());
        user.AssignRole(new UserRole(RoleType.Student));
        return user;
    }

    public static User CreateTeacher(string firstName, string lastName, string email, bool isAlsoAdmin)
    {
        var user = new User(firstName, lastName, email, new RolePolicies.TeacherRolePolicy());

        user.AssignRole(new UserRole(RoleType.Teacher));

        if (isAlsoAdmin)
        {
            user.AssignRole(new UserRole(RoleType.SchoolAdmin));
        }

        return user;
    }

    public static User CreateSchoolAdmin(string firstName, string lastName, string email)
    {
        var user = new User(firstName, lastName, email, new RolePolicies.SchoolAdminPolicy());
        user.AssignRole(new UserRole(RoleType.SchoolAdmin));
        return user;
    }

    public static User CreateAdmin(string firstName, string lastName, string email)
    {
        var user = new User(firstName, lastName, email, new RolePolicies.AdminRolePolicy());
        user.AssignRole(new UserRole(RoleType.Admin));
        return user;
    }

    public void Update(string firstName, string lastName, string email)
    {
        var name = new Name(firstName, lastName);
        Name = name;

        var userEmail = new Email(email);
        Email = userEmail;
    }

}