using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Answer.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Answer.Command;

public record SetAnswerCommand(SetAnswerRequest SetAnswerRequest)
    : IRequest<Guid>, ITransactionalCommand;

public class SetAnswerCommandHandler(
    ILogger<SetAnswerCommandHandler> logger,
    IQuestionRepository questionRepository,
    IUserRepository userRepository)
    : IRequestHandler<SetAnswerCommand, Guid>
{
  public async Task<Guid> Handle(SetAnswerCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var req = request.SetAnswerRequest;

      // Load aggregate root + editor
      var question = await questionRepository.GetQuestionByIdAsync(req.QuestionId);
      var editor = await userRepository.GetByIdAsync<Teacher>(req.EditorUserId);

      // Do — domain enforces ownership and single-answer invariant
      var answer = question.SetAnswer(editor, req.Title, req.Text);

      // Save
      await questionRepository.UpdateQuestionAsync(question);

      return answer.Id;
    }
    catch (ErrorException ex)
    {
      logger.LogError(ex, "Domain error while setting answer on question {QuestionId}. ErrorCode: {ErrorCode}",
          request.SetAnswerRequest.QuestionId, ex.ErrorCode);
      throw;
    }
    catch (Exception e)
    {
      logger.LogError(e, "Error setting answer on question {QuestionId}", request.SetAnswerRequest.QuestionId);
      throw;
    }
  }
}
