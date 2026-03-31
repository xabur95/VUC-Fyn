using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Dto.Users;
/// <summary>
/// Author: Michael
/// Repræsenterer data for en bruger hentet via ID.
/// Indeholder grundlæggende brugeroplysninger og roller.
/// </summary>

public record GetUserByIdResponse
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public IEnumerable<RoleType> Roles { get; init; } = null!;
}