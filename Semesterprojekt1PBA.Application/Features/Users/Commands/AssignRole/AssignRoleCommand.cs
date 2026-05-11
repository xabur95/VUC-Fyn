using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.AssignRole;
/// <summary>
/// Author: Michael
/// Represents a command to assign a specific role to an entity identified by a unique identifier.
/// This command is typically used in scenarios where role-based access or permissions need to be
/// managed. It encapsulates the information required to assign a role and is intended to be processed by a handler that
/// implements the corresponding business logic.</summary>
public record AssignRoleCommand : IRequest<Unit>, ITransactionalCommand
{
    public Guid Id { get; init; }
    public RoleType RoleType { get; init; }
}