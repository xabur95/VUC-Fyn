using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.AssignRole;

/// <summary>
/// Author: Michael
/// Handles the AssignRoleCommand by retrieving the user, assigning the role,
/// and persisting the changes via the repository.
/// </summary>
public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, Unit>
{
    private readonly ILogger<AssignRoleCommandHandler> _logger;
    private readonly IUserRepository _userRepository;

    public AssignRoleCommandHandler(IUserRepository userRepository, ILogger<AssignRoleCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                throw new ErrorException($"User with id '{request.Id}' was not found.", errorCode: "USER_NOT_FOUND");
            }

            user.AssignRole(new UserRole(request.RoleType));

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
        catch (ErrorException ex)
        {
            _logger.LogError(ex,
                "Domain error occurred while assigning the role. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}",
                ex.ErrorCode, ex.UserMessage);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while assigning the role.");
            throw;
        }
    }
}