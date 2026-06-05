using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Answer.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Answer.Command;

public record UpdateAnswerCommand(UpdateAnswerRequest UpdateAnswerRequest)
    : IRequest<bool>, ITransactionalCommand;

public class UpdateAnswerCommandHandler(
    ILogger<UpdateAnswerCommandHandler> logger,
    IQuestionRepository questionRepository,
    IUserRepository userRepository)
    : IRequestHandler<UpdateAnswerCommand, bool>
{
  public async Task<bool> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var req = request.UpdateAnswerRequest;

      //TODO: if the answer is being used in an assignmentSheet, it should throw ErrorException. 

      // Load aggregate root + editor
      var question = await questionRepository.GetQuestionByIdAsync(req.QuestionId);
      var editor = await userRepository.GetByIdAsync<Teacher>(req.EditorUserId);

      // Do — domain enforces ownership and answer existence
      question.UpdateAnswer(editor, req.Title, req.Text);

      // Save
      await questionRepository.UpdateQuestionAsync(question);

      return true;
    }
    catch (ErrorException ex)
    {
      logger.LogError(ex, "Domain error while updating answer on question {QuestionId}. ErrorCode: {ErrorCode}",
          request.UpdateAnswerRequest.QuestionId, ex.ErrorCode);
      throw;
    }
    catch (Exception e)
    {
      logger.LogError(e, "Error updating answer on question {QuestionId}", request.UpdateAnswerRequest.QuestionId);
      throw;
    }
  }
}
