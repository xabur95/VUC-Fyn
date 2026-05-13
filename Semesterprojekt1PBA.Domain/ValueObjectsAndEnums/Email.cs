using Semesterprojekt1PBA.Domain.Helpers;
using System.Text.RegularExpressions;

namespace Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;
/// <summary>
/// Author: Michael
/// Represents an email address value object that ensures the value is in a valid email format.
/// The value is validated upon creation to ensure it conforms to the standard email pattern.
/// </summary>
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
            throw new ErrorException($"{paramName} Must be valid email address.", errorCode: "INVALID_EMAIL");
        }
    }
}