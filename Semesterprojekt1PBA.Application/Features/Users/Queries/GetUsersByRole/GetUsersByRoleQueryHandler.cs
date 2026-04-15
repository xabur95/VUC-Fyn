using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Users;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Users.Queries.GetUsersByRole;
/// <summary>
/// Author: Michael
/// Handles the GetUsersByRoleQuery and returns user information for all users with the specified role.
/// Depends on IUserRepository for data access.
/// </summary>
public class GetUsersByRoleQueryHandler : IRequestHandler<GetUsersByRoleQuery, List<GetUsersByRoleResponse>>
{
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;

    public GetUsersByRoleQueryHandler(IUserRepository userRepository, ILogger logger)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<List<GetUsersByRoleResponse>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
    {
        try
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
        catch (ErrorException ex)
        {
            _logger.LogError(ex, "Domain error occurred while getting users by role. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}",
                ex.ErrorCode, ex.UserMessage);
            throw;
        }
        catch(Exception e)
        {
            _logger.LogError(e, "An error occurred while getting users by role.");
            throw;
        }
    }
}