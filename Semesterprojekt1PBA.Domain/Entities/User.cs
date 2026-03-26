using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;

/// <summary>
///     Author: Michael
///     Repræsenterer en bruger med identitet, kontaktoplysninger og tildelt rolle.
///     Class RolePolicy indkapsler brugerrelaterede regler til håndtering af roller.
///     User  class giver metoder til at tildele og fjerne roller samt til at opdatere brugeroplysninger.
///     Roller administreres i henhold til den angivne rollepolitik, hvilket sikrer, at kun gyldige
///     rolletildelinger er tilladt. Instanser af User oprettes typisk ved hjælp af statiske
///     fabriksmetoder, som sikrer korrekt initialisering og rolletildeling.
/// </summary>
public class User : Entity
{
    private readonly IRolePolicy _rolePolicy;
    private readonly List<UserRole> _roles = [];
    public Name Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

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

    public static User Create(string firstName, string lastName, string email, IRolePolicy rolePolicy, RoleType roleType)
    {
        var user = new User(firstName, lastName, email, rolePolicy);

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
}
