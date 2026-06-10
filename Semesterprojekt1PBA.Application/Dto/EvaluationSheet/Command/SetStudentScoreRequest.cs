namespace Semesterprojekt1PBA.Application.Dto.EvaluationSheet.Command;

public record SetStudentScoreRequest(
    Guid EvaluationSheetId,
    Guid StudentUserId,
    Guid QuestionId,
    int Points);
