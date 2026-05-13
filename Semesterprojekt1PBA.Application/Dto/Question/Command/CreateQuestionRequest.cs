using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Application.Dto.Question.Command;

public record CreateQuestionRequest(
    Guid CreatedByUserId,
    string Title,
    string Text,
    int Points,
    ActiveStatus ActiveStatus,
    Guid? ParentQuestionId,
    List<Guid>? TagIds,
    List<Guid>? SubjectIds);
