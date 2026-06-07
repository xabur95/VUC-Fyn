using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.EvaluationSheet.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.EvaluationSheet.Command;

public record SetStudentScoreCommand(SetStudentScoreRequest SetStudentScoreRequest)
    : IRequest<bool>, ITransactionalCommand;

public class SetStudentScoreCommandHandler(
    ILogger<SetStudentScoreCommandHandler> logger,
    IEvaluationSheetRepository evaluationSheetRepository,
    IUserRepository userRepository)
    : IRequestHandler<SetStudentScoreCommand, bool>
{
    public async Task<bool> Handle(SetStudentScoreCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var req = request.SetStudentScoreRequest;

            // Load aggregate root + student
            var sheet = await evaluationSheetRepository.GetByIdAsync(req.EvaluationSheetId);
            var student = await userRepository.GetByIdAsync<Student>(req.StudentUserId);

            // Do — domain enforces invariants
            sheet.SetStudentScore(student, req.QuestionId, req.Points);

            // Save
            await evaluationSheetRepository.UpdateAsync(sheet);

            return true;
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex, "Domain error while setting student score on evaluation sheet {SheetId}. ErrorCode: {ErrorCode}",
                request.SetStudentScoreRequest.EvaluationSheetId, ex.ErrorCode);
            throw;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error setting student score on evaluation sheet {SheetId}",
                request.SetStudentScoreRequest.EvaluationSheetId);
            throw;
        }
    }
}
