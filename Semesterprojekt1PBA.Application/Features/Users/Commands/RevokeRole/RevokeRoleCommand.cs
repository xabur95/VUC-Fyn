using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.RevokeRole;
/// <summary>
/// Author: Michael
/// Represents a command to revoke a specific role from an entity identified by its unique identifier.
/// This command is typically used in scenarios where role-based access or permissions need to be removed
/// from a user or resource. It is intended to be processed within a transactional context to ensure
/// consistency.</summary>
public record RevokeRoleCommand : IRequest<Unit>, ITransactionalCommand
{
    public Guid Id { get; init; }
    public RoleType RoleType { get; init; }
}