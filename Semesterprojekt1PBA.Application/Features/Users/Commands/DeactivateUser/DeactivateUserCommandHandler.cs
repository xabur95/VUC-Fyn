using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.DeactivateUser;

/// <summary>
/// Author: Michael
/// Repræsenterer en kommando til at deaktivere en brugerkonto identificeret ved et unikt ID.
/// </summary>
public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public DeactivateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        user.Deactivate();

        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}