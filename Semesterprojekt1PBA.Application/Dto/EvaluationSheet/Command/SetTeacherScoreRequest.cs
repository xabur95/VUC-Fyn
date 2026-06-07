namespace Semesterprojekt1PBA.Application.Dto.EvaluationSheet.Command;

public record SetTeacherScoreRequest(
    Guid EvaluationSheetId,
    Guid TeacherUserId,
    Guid StudentUserId,
    Guid QuestionId,
    int Points);
