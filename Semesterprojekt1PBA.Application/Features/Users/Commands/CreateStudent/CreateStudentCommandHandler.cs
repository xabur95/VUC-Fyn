using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.CreateStudent;
/// <summary>
/// Author: Michael
/// Handles the creation of a new student by processing a CreateStudentCommand
/// and persisting the entity via the user repository.
/// Returns the unique identifier of the created student.
/// </summary>
public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
{
    private readonly ILogger<CreateStudentCommandHandler> _logger;
    private readonly IUserRepository _userRepository;

    public CreateStudentCommandHandler(IUserRepository userRepository, ILogger<CreateStudentCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }
    public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = Student.Create(request.FirstName, request.LastName, request.Email, request.Knr, request.Tilmeldt, request.Ophørt);

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