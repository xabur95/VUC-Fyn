using MediatR;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.RevokeRole;
/// <summary>
/// Author: Michael
/// Håndterer kommandoen til at fjerne en specifik rolle fra en bruger.
/// Henter brugeren via ID, fjerner den specificerede rolle og opdaterer i repository'et.
/// </summary>
public class RevokeRoleCommandHandler : IRequestHandler<RevokeRoleCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public RevokeRoleCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RevokeRoleCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetByIdAsync(request.Id);

        user.RevokeRole(new UserRole(request.RoleType));

        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}