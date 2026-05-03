using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.CreateTeacher;
/// <summary>
/// Author: Michael
/// Handles the creation of a new teacher by processing a CreateTeacherCommand and returning the unique identifier of
/// the created teacher.
/// This handler coordinates the creation of a teacher entity and persists it using the provided user
/// repository. Logging is performed for both domain-specific and general exceptions encountered during the operation.
/// This class is typically used within a MediatR pipeline to process teacher creation requests.
/// </summary>

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Guid>
{
    private readonly ILogger<CreateTeacherCommandHandler> _logger;
    private readonly IUserRepository _userRepository;

    public CreateTeacherCommandHandler(IUserRepository userRepository, ILogger<CreateTeacherCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }


    public async Task<Guid> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var teacher = Teacher.Create(request.FirstName, request.LastName, request.Email);

            await _userRepository.AddAsync(teacher);

            return teacher.Id;
        }
        catch (ErrorException ex)
        {
            _logger.LogError(ex, "Domain error occurred while creating the teacher. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}", ex.ErrorCode, ex.UserMessage);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating the teacher.");
            throw;
        }
    }
}