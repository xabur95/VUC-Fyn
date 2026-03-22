using System.Text.RegularExpressions;

namespace Semesterprojekt1PBA.Domain.ValueObjects;
/// <summary>
/// Author: Michael
/// Repræsenterer en email adresse value object der sikrer værdierne er i et gyldigt email format.
/// </summary>
/// <remarks>
/// Værdien valideres ved oprettelse for at sikre den overholder standard email mønster.
/// </remarks>
public record Email
{
    public string Value { get; init; }

    public Email(string value)
    {
        AssureValidEmail(value, nameof(value));
        Value = value;
    }

    private void AssureValidEmail(string value, string paramName)
    {
        if (!Regex.IsMatch(value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
        {
            throw new ArgumentException($"{paramName} must be a valid email address.");
        }
    }
}