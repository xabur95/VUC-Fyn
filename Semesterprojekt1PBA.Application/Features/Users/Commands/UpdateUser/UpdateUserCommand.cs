using MediatR;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.UpdateUser;
/// <summary>
/// Author: Michael
/// Repræsenterer en kommando til at opdatere en eksisterende brugers oplysninger.
/// </summary>
public record UpdateUserCommand : IRequest<Unit>, ITransactionalCommand
{
    public Guid Id { get; init; }    
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
}