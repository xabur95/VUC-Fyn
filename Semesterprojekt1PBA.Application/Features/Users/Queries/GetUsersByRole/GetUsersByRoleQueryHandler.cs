using MediatR;
using Semesterprojekt1PBA.Application.Dto.Users;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Users.Queries.GetUsersByRole;
/// <summary>
/// Author: Michael
/// Håndterer GetUsersByRoleQuery og returnerer brugeroplysninger for alle brugere med den angivne rolle.
/// Afhænger af IUserRepository for adgang til brugerdata.
/// </summary>
public class GetUsersByRoleQueryHandler : IRequestHandler<GetUsersByRoleQuery, List<GetUsersByRoleResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersByRoleQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<GetUsersByRoleResponse>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetByRoleAsync(request.RoleType);

        var result = users.Select(u => new GetUsersByRoleResponse
        {
            Id = u.Id,
            FirstName = u.Name.FirstName,
            LastName = u.Name.LastName,
            Email = u.Email.Value
        }).ToList();

        return result;
    }
}