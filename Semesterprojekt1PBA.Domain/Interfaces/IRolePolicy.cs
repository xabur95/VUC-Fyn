using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Interfaces;
/// <summary>
/// Author: Michael
/// Definerer kontrakt for validering om en ny rolletildeling er tilladt baseret på brugerens nuværende roller.
/// </summary>
public interface IRolePolicy
{
    void Validate(RoleType newRole, IEnumerable<UserRole> currentRoles);
}