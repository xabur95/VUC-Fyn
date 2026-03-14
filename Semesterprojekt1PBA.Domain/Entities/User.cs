using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Entities;

public abstract class User : DomainEntity
{
    public Name Name { get; private set; } = null!;

    public Email Email { get; private set; } = null!;

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
}