using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.EvaluationSheet.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.EvaluationSheet.Command;

public record CreateEvaluationSheetCommand(CreateEvaluationSheetRequest CreateEvaluationSheetRequest)
    : IRequest<Guid>, ITransactionalCommand;

public class CreateEvaluationSheetCommandHandler(
    ILogger<CreateEvaluationSheetCommandHandler> logger,
    IEvaluationSheetRepository evaluationSheetRepository,
    IAssignmentSheetRepository assignmentSheetRepository,
    IClassRepository classRepository)
    : IRequestHandler<CreateEvaluationSheetCommand, Guid>
{
    public async Task<Guid> Handle(CreateEvaluationSheetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var req = request.CreateEvaluationSheetRequest;

            // Load
            var @class = await classRepository.GetClassByIdAsync(req.ClassId);
            var assignmentSheet = await assignmentSheetRepository.GetByIdAsync(req.AssignmentSheetId);

            // Do — snapshot the questions into a new EvaluationSheet
            var sheet = Domain.Entities.EvaluationSheet.Create(@class, assignmentSheet.Questions);

            // Save
            await evaluationSheetRepository.AddAsync(sheet);

            return sheet.Id;
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex, "Domain error occurred while creating the evaluation sheet. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}", ex.ErrorCode, ex.UserMessage);
            throw;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while creating the evaluation sheet.");
            throw;
        }
    }
}
