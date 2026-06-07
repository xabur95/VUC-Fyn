using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.EvaluationSheet.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.EvaluationSheet.Command;

public record SetTeacherScoreCommand(SetTeacherScoreRequest SetTeacherScoreRequest)
    : IRequest<bool>, ITransactionalCommand;

public class SetTeacherScoreCommandHandler(
    ILogger<SetTeacherScoreCommandHandler> logger,
    IEvaluationSheetRepository evaluationSheetRepository,
    IUserRepository userRepository)
    : IRequestHandler<SetTeacherScoreCommand, bool>
{
    public async Task<bool> Handle(SetTeacherScoreCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var req = request.SetTeacherScoreRequest;

            // Load aggregate root + actors
            var sheet = await evaluationSheetRepository.GetByIdAsync(req.EvaluationSheetId);
            var teacher = await userRepository.GetByIdAsync<Teacher>(req.TeacherUserId);
            var student = await userRepository.GetByIdAsync<Student>(req.StudentUserId);

            // Do — domain enforces invariants
            sheet.SetTeacherScore(teacher, student, req.QuestionId, req.Points);

            // Save
            await evaluationSheetRepository.UpdateAsync(sheet);

            return true;
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex, "Domain error while setting teacher score on evaluation sheet {SheetId}. ErrorCode: {ErrorCode}",
                request.SetTeacherScoreRequest.EvaluationSheetId, ex.ErrorCode);
            throw;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error setting teacher score on evaluation sheet {SheetId}",
                request.SetTeacherScoreRequest.EvaluationSheetId);
            throw;
        }
    }
}
