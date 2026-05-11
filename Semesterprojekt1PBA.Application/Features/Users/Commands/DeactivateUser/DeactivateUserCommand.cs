using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.DeactivateUser;
/// <summary>
/// Author: Michael
/// Represents a command to deactivate a user identified by a unique identifier.
/// This command is typically used in a CQRS pattern to request the deactivation of a user account. It
/// should be handled by a corresponding command handler that performs the deactivation operation within a transactional
/// context.</summary>
public record DeactivateUserCommand : IRequest<Unit>, ITransactionalCommand
{
    public Guid Id { get; init; }
}