using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Class.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Class.Command
{
  public record CreateClassCommand(CreateClassRequest CreateClassRequest)
      : IRequest<Guid>, ITransactionalCommand;

  public class CreateClassCommandHandler(
      ILogger<CreateClassCommandHandler> logger,
      IClassRepository classRepository,
      ISchoolRepository schoolRepository)
      : IRequestHandler<CreateClassCommand, Guid>
  {
    public async Task<Guid> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
      try
      {
        // Load
        var createClassRequest = request.CreateClassRequest;
        var school = await schoolRepository.GetSchoolByIdAsync(createClassRequest.SchoolId);
        var otherClasses = await classRepository.GetAllClassesInSchoolAsync(createClassRequest.SchoolId);

        // Do 
        var classToCreate = school.AddClass(createClassRequest.Title,
            createClassRequest.StartDate,
            createClassRequest.EndDate,
            otherClasses);

        // Save
        await classRepository.CreateClassAsync(classToCreate);

        return classToCreate.Id;
      }
      catch (ErrorException ex)
      {
        logger.LogError(ex, "Domain error occurred while creating the class. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}", ex.ErrorCode, ex.UserMessage);
        throw;
      }
      catch (Exception e)
      {
        logger.LogError(e, "An error occurred while creating the Class.");
        throw;
      }
    }
  }
}
