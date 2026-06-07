using System.Security.Cryptography;
using System.Text;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;
/// <summary>
/// Author: Michael
/// Represents a password value object that stores a hashed password.
/// Validates minimum length on creation and provides verification against plain text.
/// </summary>
public record Password
{
    public string HashedValue { get; init; }

    private Password(string hashedValue)
    {
        HashedValue = hashedValue;
    }

    public static Password Create(string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText) || plainText.Length < 8)
            throw new ErrorException("Password must be at least 8 characters.", errorCode: "INVALID_PASSWORD");

        return new Password(HashPassword(plainText));
    }

    public static Password FromHash(string hash) => new(hash);

    public bool Verify(string plainText) => HashedValue == HashPassword(plainText);

    private static string HashPassword(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }
}