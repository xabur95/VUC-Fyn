namespace Semesterprojekt1PBA.Application.Dto.Answer.Command;

public record SetAnswerRequest(
    Guid QuestionId,
    Guid EditorUserId,
    string Title,
    string Text);
