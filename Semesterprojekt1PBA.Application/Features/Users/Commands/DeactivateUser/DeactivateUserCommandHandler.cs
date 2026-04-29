using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.DeactivateUser;
/// <summary>
/// Author: Michael
/// Represents a command to deactivate a user account identified by a unique ID.
/// </summary>
public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, Unit>
{
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;

    public DeactivateUserCommandHandler(IUserRepository userRepository, ILogger logger)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                throw new ErrorException($"User with id '{request.Id}' was not found.", errorCode: "USER_NOT_FOUND");
            }

            user.Deactivate();

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
        catch (ErrorException ex)
        {
            _logger.LogError(ex, "Domain error occurred while deactivating the user. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}",
                ex.ErrorCode, ex.UserMessage);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while deactivating the user.");
            throw;
        }
    }
}
