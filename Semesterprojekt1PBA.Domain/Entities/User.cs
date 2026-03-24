using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.Policies;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;
/// <summary>
/// Author: Michael
/// Repræsenterer en bruger med identitet, kontaktoplysninger og tildelt rolle.
/// Userklassen indkapsler brugerrelaterede data og funktionalitet til håndtering af roller. 
/// Den giver metoder til at tildele og fjerne roller samt til at opdatere brugeroplysninger. 
/// Roller administreres i henhold til den angivne rollepolitik, hvilket sikrer, at kun gyldige 
/// rolletildelinger er tilladt. Instanser af User oprettes typisk ved hjælp af statiske 
/// fabriksmetoder, som sikrer korrekt initialisering og rolletildeling.
/// </summary>
public class User : Entity
{
    private readonly List<UserRole> _roles = [];

    private readonly IRolePolicy _rolePolicy;
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
    
    public static User CreateTeacher(string firstName, string lastName, string email, bool isAlsoAdmin)
    {
        var user = Create(firstName, lastName, email, new RolePolicies.TeacherRolePolicy(),RoleType.Teacher);

        if (isAlsoAdmin)
        {
            user.AssignRole(new UserRole(RoleType.SchoolAdmin));
        }
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