using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.School.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.School.Command;

public record CreateSchoolCommand(CreateSchoolRequest CreateSchoolRequest)
    : IRequest<Guid>, ITransactionalCommand;

public class CreateSchoolCommandHandler(
    ILogger<CreateSchoolCommandHandler> logger,
    ISchoolRepository schoolRepository)
    : IRequestHandler<CreateSchoolCommand, Guid>
{
  public async Task<Guid> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
  {
    try
    {
      // Load
      var otherSchools = await schoolRepository.GetAllSchoolsAsync();
      var createSchoolRequest = request.CreateSchoolRequest;

      // Do
      var school = Domain.Entities.School.Create(createSchoolRequest.Title, otherSchools);

      // Save
      await schoolRepository.CreateSchoolAsync(school);

      return school.Id;
    }
    catch (ErrorException ex)
    {
      logger.LogError(ex,
          "Domain error occurred while creating the school. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}",
          ex.ErrorCode, ex.UserMessage);
      throw;
    }
    catch (Exception e)
    {
      logger.LogError(e, "An error occurred while creating the school.");
      throw;
    }
  }
}