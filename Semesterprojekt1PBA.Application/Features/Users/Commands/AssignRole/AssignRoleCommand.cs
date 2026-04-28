using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.AssignRole;
/// <summary>
/// Author: Michael
/// Repræsenterer en kommando til at tildele en specifik rolle til en bruger eller entitet i systemet.
/// Denne kommando bruges typisk i scenarier hvor rollebaseret adgang eller rettigheder skal opdateres.
/// </summary>
public record AssignRoleCommand : IRequest<Unit>, ITransactionalCommand
{
    public Guid Id { get; init; }
    public RoleType RoleType { get; init; }
}