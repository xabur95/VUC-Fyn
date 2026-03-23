namespace Semesterprojekt1PBA.Domain.ValueObjects;
/// <summary>
/// Author: Michael
/// Repræsenterer en brugers rolle i systemet.
/// </summary>
/// <remarks>
/// Brug denne record til at knytte en rolle til en bruger. Rollen bestemmer hvilke rettigheder og muligheder
/// brugeren har.
/// </remarks>
public record UserRole
{
    public RoleType RoleType { get; }

    private UserRole()
    {
    }

    public UserRole(RoleType roleType)
    {
        RoleType = roleType;
    }
}