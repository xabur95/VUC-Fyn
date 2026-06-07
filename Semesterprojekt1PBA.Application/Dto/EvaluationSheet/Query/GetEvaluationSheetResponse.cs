namespace Semesterprojekt1PBA.Application.Dto.EvaluationSheet.Query;

public record GetEvaluationSheetResponse(
    Guid Id,
    byte[] RowVersion,
    Guid ClassId,
    IEnumerable<Guid> QuestionIds,
    IEnumerable<GetQuestionScoreResponse> TeacherScores,
    IEnumerable<GetQuestionScoreResponse> StudentScores);

public record GetQuestionScoreResponse(
    Guid Id,
    Guid StudentId,
    Guid QuestionId,
    int Points,
    Guid ScoredByUserId);
