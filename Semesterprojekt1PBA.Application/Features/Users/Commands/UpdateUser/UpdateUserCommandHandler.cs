using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.UpdateUser;

/// <summary>
/// Author: Michael
/// Handles the update of a user by processing an UpdateUserCommand.
/// Retrieves the user by ID, applies the updates, and persists the changes via the repository.
/// </summary>
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly ILogger<UpdateUserCommandHandler> _logger;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository, ILogger<UpdateUserCommandHandler> logger)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                throw new ErrorException($"User with id '{request.Id}' was not found.", "USER_NOT_FOUND");
            }

            user.Update(request.FirstName, request.LastName, request.Email);

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
        catch (ErrorException ex)
        {
            _logger.LogError(ex,
                "Domain error occurred while updating the user. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}",
                ex.ErrorCode, ex.UserMessage);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while updating the user.");
            throw;
        }
    }
}