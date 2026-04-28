using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Interfaces;
/// <summary>
/// Author: Michael
/// Defines the contract for validating whether a new role assignment is permitted
/// based on the user's current roles.
/// </summary>
public interface IRolePolicy
{
    void Validate(RoleType newRole, IEnumerable<UserRole> currentRoles);
}