using FluentResults;
using MediatR;
using Semesterprojekt1PBA.Application.Dto.Answer.Query;

namespace Semesterprojekt1PBA.Application.Features.Answer.Query;

/// <summary>
/// Retrieves the answer for a given question. Since Answer lives inside
/// the Question aggregate, we load via QuestionId.
/// </summary>
public record GetAnswerByQuestionIdQuery(Guid QuestionId)
    : IRequest<Result<GetAnswerResponse>>;
