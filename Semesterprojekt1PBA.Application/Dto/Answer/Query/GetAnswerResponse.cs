namespace Semesterprojekt1PBA.Application.Dto.Answer.Query;

public record GetAnswerResponse(
    Guid Id,
    string Title,
    string Text,
    Guid QuestionId,
    Guid CreatedByUserId);
