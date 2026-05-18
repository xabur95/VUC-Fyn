using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Answer.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Answer.Command;

public record RemoveAnswerCommand(RemoveAnswerRequest RemoveAnswerRequest)
    : IRequest<bool>, ITransactionalCommand;

public class RemoveAnswerCommandHandler(
    ILogger<RemoveAnswerCommandHandler> logger,
    IQuestionRepository questionRepository,
    IUserRepository userRepository)
    : IRequestHandler<RemoveAnswerCommand, bool>
{
  public async Task<bool> Handle(RemoveAnswerCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var req = request.RemoveAnswerRequest;

      //TODO: if the question is being used in any AssignmentSheet, then it should become inactive

      // Load aggregate root + editor
      var question = await questionRepository.GetQuestionByIdAsync(req.QuestionId);
      var editor = await userRepository.GetByIdAsync<Teacher>(req.EditorUserId);

      // Do — domain enforces ownership
      question.RemoveAnswer(editor);

      // Save
      await questionRepository.UpdateQuestionAsync(question);

      return true;
    }
    catch (ErrorException ex)
    {
      logger.LogError(ex, "Domain error while removing answer on question {QuestionId}. ErrorCode: {ErrorCode}",
          request.RemoveAnswerRequest.QuestionId, ex.ErrorCode);
      return false;
    }
    catch (Exception e)
    {
      logger.LogError(e, "Error removing answer on question {QuestionId}", request.RemoveAnswerRequest.QuestionId);
      return false;
    }
  }
}
