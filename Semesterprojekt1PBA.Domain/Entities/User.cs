using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;
/// <summary>
/// Author: Michael
/// Repræsenterer en abstrakt bruger og fælles funktioner for identitet og roller.
/// </summary>
/// <remarks>
/// Klassen er base for bruger typer som student, lærer og Admin. Den indeholder fælles egenskaber og kræver at
/// nedarvede klasser håndterer tildeling og fjernelse af roller. Metoder til rolle styring er beskyttede og bruges
/// kun i nedarvede klasser.
/// </remarks>
public abstract class User : DomainEntity
{
    public Name Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;

    private readonly List<UserRole> _roles = new();
    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();


    protected User()
    {
    }

    protected User(string firstName, string lastName, string email)
    {
        var name = new Name(firstName, lastName);
        Name = name;

        var userEmail = new Email(email);
        Email = userEmail;

        Id = Guid.NewGuid();
    }

    // Ligges her så det er tilgængeligt for alle nedarvede klasser
    // Hvis de flyttes ud på entitet niveau er koden DRY, da både Student, Teacher og Admin skal have samme kode for at opdatere navn og email 
    public void Update(string firstName, string lastName, string email)
    {
        var name = new Name(firstName, lastName);
        Name = name;

        var userEmail = new Email(email);
        Email = userEmail;
    }

    protected void AddRole(UserRole role)
    {
        _roles.Add(role);
    }

    protected void RemoveRole(UserRole role)
    {
        _roles.Remove(role);
    }

    // Tvinger at Student, Teacher og Admin skal opsætte regler for deres egen Rolle
    public abstract void RevokeRole(UserRole role);


    // Tvinger at Student, Teacher og Admin skal opsætte regler for deres egen Rolle
    public abstract void AssignRole(UserRole role);
}