namespace Semesterprojekt1PBA.Application.Dto.Answer.Command;

public record UpdateAnswerRequest(
    Guid QuestionId,
    Guid EditorUserId,
    byte[] RowVersion,
    string Title,
    string Text);
