using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.School.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.School.Command;

public record UpdateSchoolTitleCommand(UpdateSchoolTitleRequest UpdateSchoolTitleRequest)
    : IRequest<Result<bool>>, ITransactionalCommand;

public class UpdateSchoolCommandHandler(
    ILogger<UpdateSchoolCommandHandler> logger,
    ISchoolRepository schoolRepository)
    : IRequestHandler<UpdateSchoolTitleCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateSchoolTitleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load 
            var updateSchoolTitleRequest = request.UpdateSchoolTitleRequest;
            var school = await schoolRepository.GetSchoolByIdAsync(updateSchoolTitleRequest.Id);
            var otherSchools = await schoolRepository.GetAllSchoolsAsync();


            // Do
            school.UpdateTitle(updateSchoolTitleRequest.Title, otherSchools);

            // Save
            await schoolRepository.UpdateSchoolTitleAsync(school);

            return Result.Ok().WithSuccess("School Title has been updated");
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex,
                "Domain error occurred while updating the school title. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}",
                ex.ErrorCode, ex.UserMessage);
            return Result.Fail(ex.UserMessage ?? "Failed to update School title due to a domain error.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating school title with id {Id}", request.UpdateSchoolTitleRequest.Id);
            return Result.Fail(
                "Something went wrong while updating the school title. If the error happens again, contact support!");
        }
    }
}