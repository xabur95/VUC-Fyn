using System.Text.RegularExpressions;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;
/// <summary>
/// Author: Michael
/// Represents a person's name with validated first name and last name components.
/// First name and last name are validated to ensure they are not empty, contain only alphabetic characters,
/// and are between 2 and 40 characters in length. This record is immutable after creation.
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
            throw new ErrorException($"{paramName} cannot be null or whitespace.", errorCode: "INVALID_NAME");
        }
    }

    private void AssureNoSpecialCharacters(string value, string paramName)
    {
        if (!Regex.IsMatch(value, "^[a-zA-Z]+$"))
        {
            throw new ErrorException($"{paramName} cannot contain special characters.", errorCode: "INVALID_NAME");
        }
    }

    private void AssureLength(string value, int min, int max, string paramName)
    {
        if (value.Length < min || value.Length > max)
        {
            throw new ErrorException($"{paramName} must be between {min} and {max} characters long", errorCode: "INVALID_NAME");
        }
    }
}