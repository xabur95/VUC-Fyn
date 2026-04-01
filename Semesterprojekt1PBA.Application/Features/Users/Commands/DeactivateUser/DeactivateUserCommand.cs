using MediatR;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.DeactivateUser;
/// <summary>
/// Author: Michael
/// Repræsenterer en kommando til at deaktivere en brugerkonto identificeret ved et unikt ID.
/// </summary>
public record DeactivateUserCommand : IRequest<Unit>, ITransactionalCommand
{
    public Guid Id { get; init; }
}