using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Answer.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Answer.Command;

public record UpdateAnswerCommand(UpdateAnswerRequest UpdateAnswerRequest)
    : IRequest<Result<bool>>, ITransactionalCommand;

public class UpdateAnswerCommandHandler(
    ILogger logger,
    IQuestionRepository questionRepository,
    IUserRepository userRepository)
    : IRequestHandler<UpdateAnswerCommand, Result<bool>>
{
  public async Task<Result<bool>> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var req = request.UpdateAnswerRequest;

      //TODO: if the answer is being used in an assignmentSheet, it should throw ErrorException. 

      // Load aggregate root + editor
      var question = await questionRepository.GetQuestionByIdAsync(req.QuestionId);
      var editor = await userRepository.GetByIdAsync(req.EditorUserId);

      // Do — domain enforces ownership and answer existence
      question.UpdateAnswer(editor, req.Title, req.Text);

      // Save
      await questionRepository.UpdateQuestionAsync(question);

      return Result.Ok().WithSuccess("Answer updated successfully.");
    }
    catch (ErrorException ex)
    {
      logger.LogError(ex, "Domain error while updating answer on question {QuestionId}. ErrorCode: {ErrorCode}",
          request.UpdateAnswerRequest.QuestionId, ex.ErrorCode);
      return Result.Fail(ex.UserMessage ?? "Failed to update Answer due to a domain error.");
    }
    catch (Exception e)
    {
      logger.LogError(e, "Error updating answer on question {QuestionId}", request.UpdateAnswerRequest.QuestionId);
      return Result.Fail("Something went wrong while updating the answer.");
    }
  }
}
