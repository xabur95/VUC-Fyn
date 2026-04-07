using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Abstractions;
using Semesterprojekt1PBA.Application.Dto.Class.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Application.Features.Class.Command
{
    public record CreateClassCommand(CreateClassRequest CreateClassRequest)
        : IRequest<Result<bool>>, ITransactionalCommand;

    public class CreateClassCommandHandler(
        ILogger logger,
        IClassRepository classRepository,
        ISchoolRepository schoolRepository)
        : IRequestHandler<CreateClassCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
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

                return Result.Ok().WithSuccess("Class has been created");
            }
            catch (ErrorException ex)
            {
                logger.LogError(ex, "Domain error occurred while creating the class. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}", ex.ErrorCode, ex.UserMessage);
                return Result.Fail(ex.UserMessage ?? "Failed to create Class due to a domain error.");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while creating the Class.");
                return Result.Fail("Something went wrong while creating the class. If the error happens again, contact support!");
            }
        }
    }
}
