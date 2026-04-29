using MediatR;
using Semesterprojekt1PBA.Application.Dto.Users;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Features.Users.Queries.GetUsersByRole;
/// <summary>
/// Author: Michael
/// Repræsenterer en query til at hente alle brugere med en bestemt rolle via MediatR.
/// Anvendes typisk i rollebaserede adgangs- eller administrationssenarier.
/// </summary>

public record GetUsersByRoleQuery : IRequest<List<GetUsersByRoleResponse>>
{
    public RoleType RoleType { get; init; }
}