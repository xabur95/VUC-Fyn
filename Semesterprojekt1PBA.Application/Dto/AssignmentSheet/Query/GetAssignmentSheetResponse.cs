namespace Semesterprojekt1PBA.Application.Dto.AssignmentSheet.Query;

public record GetAssignmentSheetResponse(
    Guid Id,
    byte[] RowVersion,
    Guid AuthorId,
    Guid SubjectId,
    string SubjectTitle,
    IEnumerable<GetAssignmentSheetTopicResponse> Topics,
    IEnumerable<GetAssignmentSheetQuestionResponse> Questions);

public record GetAssignmentSheetTopicResponse(
    Guid Id,
    string Name);

public record GetAssignmentSheetQuestionResponse(
    Guid Id,
    string Title,
    string Text,
    int Points);
