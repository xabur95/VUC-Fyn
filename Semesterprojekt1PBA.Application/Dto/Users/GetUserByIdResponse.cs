using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Dto.Users;
/// <summary>
/// Author: Michael
/// Represents data for a user retrieved by ID.
/// Contains basic user information and roles.
/// </summary>
public record GetUserByIdResponse
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public IEnumerable<RoleType> Roles { get; init; } = null!;
}