using MediatR;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.AssignRole;
/// <summary>
/// Author: Michael
/// Håndterer kommandoen til at tildele en rolle til en bruger ved at opdatere brugerens rolleinformation i repository'et.
/// Behandler AssignRoleCommand ved at hente brugeren, tildele rollen og gemme ændringerne.
/// </summary>
public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public AssignRoleCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        user.AssignRole(new UserRole(request.RoleType));

        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}