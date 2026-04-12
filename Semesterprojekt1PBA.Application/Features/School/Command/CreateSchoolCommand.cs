using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.School.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Application.Features.School.Command
{
    public record CreateSchoolCommand(CreateSchoolRequest CreateSchoolRequest)
        : IRequest<Result<bool>>, ITransactionalCommand;

    public class CreateSchoolCommandHandler(
        ILogger logger,
        ISchoolRepository schoolRepository)
        : IRequestHandler<CreateSchoolCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
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

                return Result.Ok().WithSuccess("School created successfully.");
            }
            catch (ErrorException ex)
            {
                logger.LogError(ex, "Domain error occurred while creating the school. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}", ex.ErrorCode, ex.UserMessage);
                return Result.Fail(ex.UserMessage ?? "Failed to create School due to a domain error.");
            }   
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while creating the school.");
                return Result.Fail("Failed to create School");
            }

        }
    }
}
