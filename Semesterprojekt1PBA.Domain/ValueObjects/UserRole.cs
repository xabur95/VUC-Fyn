namespace Semesterprojekt1PBA.Domain.ValueObjects;
/// <summary>
/// Author: Michael
/// Repræsenterer en brugers rolle i systemet.
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