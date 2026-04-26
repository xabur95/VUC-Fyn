using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.CreateAdmin;
/// <summary>
/// Author: Michael
/// Represents a command to create a new admin with the specified details.
/// This command is typically used in a CQRS pattern to encapsulate the data required to create a admin
/// entity. The command returns the unique identifier of the newly created teacher upon successful execution.
/// </summary>
public record CreateAdminCommand : IRequest<Guid>, ITransactionalCommand
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
}