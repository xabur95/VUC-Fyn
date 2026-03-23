using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Interfaces;

public interface IRolePolicy
{
    void Validate(RoleType newRole, IEnumerable<UserRole> currentRoles);
}