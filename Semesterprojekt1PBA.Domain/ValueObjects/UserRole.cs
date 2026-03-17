namespace Semesterprojekt1PBA.Domain.ValueObjects;

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