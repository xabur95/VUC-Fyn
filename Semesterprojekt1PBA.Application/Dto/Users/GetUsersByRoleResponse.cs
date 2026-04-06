namespace Semesterprojekt1PBA.Application.Dto.Users;
/// <summary>
/// Author: Michael
/// Repræsenterer en bruger returneret af en rolle-baseret query.
/// </summary>
public record GetUsersByRoleResponse
{ public Guid Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
}