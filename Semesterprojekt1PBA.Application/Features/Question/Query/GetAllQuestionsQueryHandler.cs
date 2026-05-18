using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Question.Query;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;

namespace Semesterprojekt1PBA.Application.Features.Question.Query;

public class GetAllQuestionsQueryHandler(
    ILogger<GetAllQuestionsQueryHandler> logger,
    IQuestionRepository questionRepository)
    : IRequestHandler<GetAllQuestionsQuery, IEnumerable<GetQuestionResponse>>
{
  public async Task<IEnumerable<GetQuestionResponse>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
  {
    try
    {
      var questions = await questionRepository.GetAllQuestionsAsync();

      var responses = questions.Select(q => new GetQuestionResponse(
          q.Id,
          q.RowVersion,
          q.Title.Value,
          q.Text,
          q.Points,
          q.ActiveStatus,
          q.CreatedByUserId,
          q.ParentQuestion?.Id,
          q.Tags.Select(t => new GetQuestionTagResponse(t.Id, t.Title.Value)),
          q.Subjects.Select(s => new GetQuestionSubjectResponse(s.Id, s.Title)),
          q.Answer is not null
              ? new GetQuestionAnswerResponse(
                  q.Answer.Id,
                  q.Answer.Title.Value,
                  q.Answer.Text,
                  q.Answer.CreatedByUserId)
              : null));

      return responses;
    }
    catch (Exception e)
    {
      logger.LogError(e, "Error fetching all questions.");
      throw;
    }
  }
}
