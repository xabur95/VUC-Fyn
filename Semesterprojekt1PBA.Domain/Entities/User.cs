using System.Text.RegularExpressions;

namespace Semesterprojekt1PBA.Domain.Entities;

public abstract class User : DomainEntity
{
    public string FirstName { get; protected set; } = null!;
    public string LastName { get; protected set; } = null!;
    public string Email { get; protected set; } = null!;

    private User()
    {
    }

    protected User(string firstName, string lastName, string email)
    {
        AssureFirstName(firstName);
        AssureLastName(lastName);
        AssureValidEmail(email);

        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    protected void AssureFirstName(string firstName)
    {
        AssureNotEmpty(firstName);
        AssureNoSpecialCharacters(firstName);
        AssureLength(firstName, 2, 40);
    }

    protected void AssureLastName(string lastName)
    {
        AssureNotEmpty(lastName);
        AssureNoSpecialCharacters(lastName);
        AssureLength(lastName, 2, 40);
    }

    private void AssureNotEmpty(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
        }
    }

    private void AssureNoSpecialCharacters(string value)
    {
        if (!Regex.IsMatch(value, "^[a-zA-Z]+$"))
        {
            throw new ArgumentException("Value cannot contain special characters.", nameof(value));
        }
    }

    private void AssureLength(string value, int min, int max)
    {
        if (value.Length < min || value.Length > max)
        {
            throw new ArgumentException($"Value must be between {min} and {max} characters long.", nameof(value));
        }
    }

    private void AssureValidEmail(string value)
    {
        if (!Regex.IsMatch(value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
        {
            throw new ArgumentException("Value must be a valid email address.", nameof(value));
        }
    }
}