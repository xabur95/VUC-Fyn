using FluentResults;
using MediatR;
using Semesterprojekt1PBA.Application.Dto.Question.Query;

namespace Semesterprojekt1PBA.Application.Features.Question.Query;

public record GetQuestionByIdQuery(Guid QuestionId)
    : IRequest<Result<GetQuestionResponse>>;
