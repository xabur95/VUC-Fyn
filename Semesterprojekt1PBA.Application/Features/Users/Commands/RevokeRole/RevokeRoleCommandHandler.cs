using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.RevokeRole;
/// <summary>
/// Author: Michael
/// Handles the command to revoke a specific role from a user.
/// Retrieves the user by ID, removes the role, and persists the changes via the repository.
/// </summary>
public class RevokeRoleCommandHandler : IRequestHandler<RevokeRoleCommand, Unit>
{
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;

    public RevokeRoleCommandHandler(IUserRepository userRepository, ILogger logger)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RevokeRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            User user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                throw new ErrorException($"User with id '{request.Id}' was not found.", errorCode: "USER_NOT_FOUND");
            }

            user.RevokeRole(new UserRole(request.RoleType));

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
        catch (ErrorException ex)
        {
            _logger.LogError(ex, "Error occurred while revoking user role. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}",
                ex.ErrorCode, ex.UserMessage);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while revoking user role.");
            throw;
        }
    }
}