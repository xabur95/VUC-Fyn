using System.Text.RegularExpressions;

namespace Semesterprojekt1PBA.Domain.ValueObjects;
/// <summary>
/// Author: Michael
/// Repræsenterer en persons navn med valideret fornavn og efternavn komponenter.
/// Fornavn og efternavn bliver valideret for at sikre de ikke er tomme, indeholder kun alfabetiske tegn,
/// og er mellem 2 og 40 tegn i længde. Denne record er immutable efter oprettelse.
/// </summary>

public record Name
{
    public string FirstName { get; init; }
    public string LastName { get; init; }

    public Name(string firstName, string lastName)
    {
        AssureFirstName(firstName);
        AssureLastName(lastName);
        FirstName = firstName;
        LastName = lastName;
    }

    private void AssureFirstName(string firstName)
    {
        AssureNotEmpty(firstName, nameof(firstName));
        AssureNoSpecialCharacters(firstName, nameof(firstName));
        AssureLength(firstName, 2, 40, nameof(firstName));
    }

    private void AssureLastName(string lastName)
    {
        AssureNotEmpty(lastName, nameof(lastName));
        AssureNoSpecialCharacters(lastName, nameof(lastName));
        AssureLength(lastName, 2, 40, nameof(lastName));
    }

    private void AssureNotEmpty(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{paramName} cannot be null or whitespace.");
        }
    }

    private void AssureNoSpecialCharacters(string value, string paramName)
    {
        if (!Regex.IsMatch(value, "^[a-zA-Z]+$"))
        {
            throw new ArgumentException($"{paramName} cannot contain special characters.");
        }
    }

    private void AssureLength(string value, int min, int max, string paramName)
    {
        if (value.Length < min || value.Length > max)
        {
            throw new ArgumentException($"{paramName} must be between {min} and {max} characters long.");
        }
    }
}