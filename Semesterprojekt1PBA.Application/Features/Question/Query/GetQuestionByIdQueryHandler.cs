using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Question.Query;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Question.Query;

public class GetQuestionByIdQueryHandler(
    ILogger logger,
    IQuestionRepository questionRepository)
    : IRequestHandler<GetQuestionByIdQuery, Result<GetQuestionResponse>>
{
    public async Task<Result<GetQuestionResponse>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var question = await questionRepository.GetQuestionByIdAsync(request.QuestionId);

            var response = new GetQuestionResponse(
                question.Id,
                question.RowVersion,
                question.Title.Value,
                question.Text,
                question.Points,
                question.ActiveStatus,
                question.CreatedByUserId,
                question.ParentQuestion?.Id,
                question.Tags.Select(t => new GetQuestionTagResponse(t.Id, t.Title.Value)),
                question.Subjects.Select(s => new GetQuestionSubjectResponse(s.Id, s.Name)),
                question.Answer is not null
                    ? new GetQuestionAnswerResponse(
                        question.Answer.Id,
                        question.Answer.Title.Value,
                        question.Answer.Text,
                        question.Answer.CreatedByUserId)
                    : null);

            return Result.Ok(response);
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex, "Domain error occurred while fetching question {Id}. ErrorCode: {ErrorCode}", request.QuestionId, ex.ErrorCode);
            return Result.Fail(ex.UserMessage ?? "Failed to retrieve the question.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching question with id {Id}", request.QuestionId);
            return Result.Fail("Something went wrong while fetching the question.");
        }
    }
}
