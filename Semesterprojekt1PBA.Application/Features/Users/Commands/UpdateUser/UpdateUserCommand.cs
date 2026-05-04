using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.UpdateUser;
/// <summary>
/// Author: Michael
/// Represents a command to update the details of an existing user.
/// This command is typically used in a CQRS pattern to request an update to a user's profile
/// information, such as first name, last name, or email address. The command should be handled by a corresponding
/// handler that performs the update operation within a transactional context.</summary>
public record UpdateUserCommand : IRequest<Unit>, ITransactionalCommand
{
    public Guid Id { get; init; }    
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
}