using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.CreateAdmin;
/// <summary>
/// Author: Michael
/// Handles the creation of a new admin by processing a CreateAdminCommand and returning the unique identifier of
/// the created admin. This handler coordinates the creation of a admin entity and persists it using the provided user
/// repository. Logging is performed for both domain-specific and general exceptions encountered during the operation.
/// This class is typically used within a MediatR pipeline to process admin creation requests.
/// </summary>
public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateAdminCommandHandler> _logger;
   

    public CreateAdminCommandHandler(IUserRepository userRepository, ILogger<CreateAdminCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var admin = Admin.Create(request.FirstName, request.LastName, request.Email);

            await _userRepository.AddAsync(admin);

            return admin.Id;
        }
        catch (ErrorException ex)
        {
            _logger.LogError(ex, "Domain error occurred while creating the admin. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}", ex.ErrorCode, ex.UserMessage);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating the admin.");
            throw;
        }
    }
}