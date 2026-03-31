using MediatR;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.CreateUser;
/// <summary>
/// Author: Michael
/// Opretter en ny bruger med de angivne oplysninger.
/// Returnerer ID'et på den oprettede bruger.
/// </summary>

public record CreateUserCommand : IRequest<Guid>, ITransactionalCommand
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public RoleType RoleType { get; init; }
}