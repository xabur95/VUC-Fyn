using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Application.Dto.Question.Command;

public record UpdateQuestionRequest(
    Guid QuestionId,
    Guid EditorUserId,
    byte[] RowVersion,
    string Title,
    string Text,
    int Points,
    ActiveStatus ActiveStatus);
