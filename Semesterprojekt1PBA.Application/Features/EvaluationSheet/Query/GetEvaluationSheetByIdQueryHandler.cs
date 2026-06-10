using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.EvaluationSheet.Query;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.EvaluationSheet.Query;

public class GetEvaluationSheetByIdQueryHandler(
    ILogger<GetEvaluationSheetByIdQueryHandler> logger,
    IEvaluationSheetRepository evaluationSheetRepository)
    : IRequestHandler<GetEvaluationSheetByIdQuery, GetEvaluationSheetResponse>
{
    public async Task<GetEvaluationSheetResponse> Handle(GetEvaluationSheetByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var sheet = await evaluationSheetRepository.GetByIdAsync(request.EvaluationSheetId);

            return new GetEvaluationSheetResponse(
                sheet.Id,
                sheet.RowVersion,
                sheet.Class.Id,
                sheet.QuestionIds,
                sheet.TeacherScores.Select(s => new GetQuestionScoreResponse(
                    s.Id, s.StudentId, s.QuestionId, s.Points, s.ScoredByUserId)),
                sheet.StudentScores.Select(s => new GetQuestionScoreResponse(
                    s.Id, s.StudentId, s.QuestionId, s.Points, s.ScoredByUserId)));
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex, "Domain error occurred while fetching evaluation sheet {Id}. ErrorCode: {ErrorCode}",
                request.EvaluationSheetId, ex.ErrorCode);
            throw;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching evaluation sheet with id {Id}", request.EvaluationSheetId);
            throw;
        }
    }
}
