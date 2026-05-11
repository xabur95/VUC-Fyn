using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Answer.Query;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Answer.Query;

public class GetAnswerByQuestionIdQueryHandler(
    ILogger logger,
    IQuestionRepository questionRepository)
    : IRequestHandler<GetAnswerByQuestionIdQuery, GetAnswerResponse>
{
  public async Task<GetAnswerResponse> Handle(GetAnswerByQuestionIdQuery request, CancellationToken cancellationToken)
  {
    try
    {
      var question = await questionRepository.GetQuestionByIdAsync(request.QuestionId);

      if (question.Answer is null)
        throw new ErrorException("This question does not have an answer yet.");

      var answer = question.Answer;

      var response = new GetAnswerResponse(
          answer.Id,
          answer.Title.Value,
          answer.Text,
          answer.QuestionId,
          answer.CreatedByUserId);

      return response;
    }
    catch (ErrorException ex)
    {
      logger.LogError(ex, "Domain error fetching answer for question {QuestionId}. ErrorCode: {ErrorCode}",
          request.QuestionId, ex.ErrorCode);
      throw;
    }
    catch (Exception e)
    {
      logger.LogError(e, "Error fetching answer for question {QuestionId}", request.QuestionId);
      throw;
    }
  }
}
