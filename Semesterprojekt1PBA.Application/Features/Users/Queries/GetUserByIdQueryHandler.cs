using MediatR;
using Semesterprojekt1PBA.Application.Dto.Users;
using Semesterprojekt1PBA.Domain.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Users.Queries;
/// <summary>
/// Author: MIchael
/// Håndterer forespørgsler om at hente en bruger via ID.
/// Returnerer brugerens oplysninger.
/// </summary>

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        var result = new GetUserByIdResponse
        {
            Id = user.Id,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Email = user.Email.Value,
            Roles = user.Roles.Select(r => r.RoleType)
        };

        return result;
    }
}