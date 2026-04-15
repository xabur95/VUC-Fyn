namespace Semesterprojekt1PBA.Domain.ValueObjects;
/// <summary>
/// Author: Michael
/// Represents a user's role in the system.
/// </summary>
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