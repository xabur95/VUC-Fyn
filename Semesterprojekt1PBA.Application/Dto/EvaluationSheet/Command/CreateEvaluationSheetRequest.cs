namespace Semesterprojekt1PBA.Application.Dto.EvaluationSheet.Command;

public record CreateEvaluationSheetRequest(
    Guid ClassId,
    Guid AssignmentSheetId);
