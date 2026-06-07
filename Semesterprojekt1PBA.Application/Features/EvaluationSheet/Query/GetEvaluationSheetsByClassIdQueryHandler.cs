using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.EvaluationSheet.Query;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;

namespace Semesterprojekt1PBA.Application.Features.EvaluationSheet.Query;

public class GetEvaluationSheetsByClassIdQueryHandler(
    ILogger<GetEvaluationSheetsByClassIdQueryHandler> logger,
    IEvaluationSheetRepository evaluationSheetRepository)
    : IRequestHandler<GetEvaluationSheetsByClassIdQuery, IEnumerable<GetEvaluationSheetResponse>>
{
    public async Task<IEnumerable<GetEvaluationSheetResponse>> Handle(GetEvaluationSheetsByClassIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var sheets = await evaluationSheetRepository.GetByClassIdAsync(request.ClassId);

            return sheets.Select(sheet => new GetEvaluationSheetResponse(
                sheet.Id,
                sheet.RowVersion,
                sheet.Class.Id,
                sheet.QuestionIds,
                sheet.TeacherScores.Select(s => new GetQuestionScoreResponse(
                    s.Id, s.StudentId, s.QuestionId, s.Points, s.ScoredByUserId)),
                sheet.StudentScores.Select(s => new GetQuestionScoreResponse(
                    s.Id, s.StudentId, s.QuestionId, s.Points, s.ScoredByUserId))));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching evaluation sheets for class {ClassId}.", request.ClassId);
            throw;
        }
    }
}
