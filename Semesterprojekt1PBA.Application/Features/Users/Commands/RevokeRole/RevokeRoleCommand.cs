using MediatR;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.RevokeRole;
/// <summary>
/// Author: Michael
/// Repræsenterer en kommando til at fjerne en specifik rolle fra en entitet identificeret ved dens unikke ID.
/// </summary>
public record RevokeRoleCommand : IRequest<Unit>, ITransactionalCommand
{
    public Guid Id { get; init; }
    public RoleType RoleType { get; init; }
}