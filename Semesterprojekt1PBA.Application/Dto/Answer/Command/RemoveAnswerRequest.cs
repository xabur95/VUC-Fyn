namespace Semesterprojekt1PBA.Application.Dto.Answer.Command;

public record RemoveAnswerRequest(
    Guid QuestionId,
    Guid EditorUserId,
    byte[] RowVersion);
