using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.CreateUser;
/// <summary>
/// Author: Michael
/// Handles the creation of a new user via a Command.
/// Processes a CreateUserCommand and persists the user through a repository.
/// </summary>

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository, ILogger logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            User user = User.Create(request.FirstName, request.LastName, request.Email, request.RoleType);

            await _userRepository.AddAsync(user);

            return user.Id;
        }
        catch (ErrorException ex)
        {
            _logger.LogError(ex, "Domain error occurred while creating the user. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}", ex.ErrorCode, ex.UserMessage);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating the user.");
            throw;
        }
    }
}