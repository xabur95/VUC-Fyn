using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Users;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Users.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
{
    private readonly ILogger<LoginQueryHandler> _logger;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IUserRepository userRepository, ILogger<LoginQueryHandler> logger)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
            throw new ErrorException("Invalid email or password.", errorCode: "INVALID_CREDENTIALS");

        if (!user.Password.Verify(request.Password))
            throw new ErrorException("Invalid email or password.", errorCode: "INVALID_CREDENTIALS");

        return new LoginResponse
        {
            Id = user.Id,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Email = user.Email.Value,
            Roles = user.Roles.Select(r => r.RoleType)
        };
    }
}
