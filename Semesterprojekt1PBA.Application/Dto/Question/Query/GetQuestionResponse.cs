using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Application.Dto.Question.Query;

public record GetQuestionResponse(
    Guid Id,
    byte[] RowVersion,
    string Title,
    string Text,
    int Points,
    ActiveStatus ActiveStatus,
    Guid CreatedByUserId,
    Guid? ParentQuestionId,
    IEnumerable<GetQuestionTagResponse> Tags,
    IEnumerable<GetQuestionSubjectResponse> Subjects,
    GetQuestionAnswerResponse? Answer);

public record GetQuestionTagResponse(
    Guid Id,
    string Title);

public record GetQuestionSubjectResponse(
    Guid Id,
    string Name);

public record GetQuestionAnswerResponse(
    Guid Id,
    string Title,
    string Text,
    Guid CreatedByUserId);
