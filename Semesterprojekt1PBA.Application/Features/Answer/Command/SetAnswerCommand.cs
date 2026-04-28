using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Answer.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Answer.Command;

public record SetAnswerCommand(SetAnswerRequest SetAnswerRequest)
    : IRequest<Result<Guid>>, ITransactionalCommand;

public class SetAnswerCommandHandler(
    ILogger logger,
    IQuestionRepository questionRepository,
    IUserRepository userRepository)
    : IRequestHandler<SetAnswerCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(SetAnswerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var req = request.SetAnswerRequest;

            // Load aggregate root + editor
            var question = await questionRepository.GetQuestionByIdAsync(req.QuestionId);
            var editor = await userRepository.GetByIdAsync(req.EditorUserId);

            // Do — domain enforces ownership and single-answer invariant
            var answer = question.SetAnswer(editor, req.Title, req.Text);

            // Save
            await questionRepository.UpdateQuestionAsync(question);

            return Result.Ok(answer.Id).WithSuccess("Answer created successfully.");
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex, "Domain error while setting answer on question {QuestionId}. ErrorCode: {ErrorCode}",
                request.SetAnswerRequest.QuestionId, ex.ErrorCode);
            return Result.Fail(ex.UserMessage ?? "Failed to set Answer due to a domain error.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error setting answer on question {QuestionId}", request.SetAnswerRequest.QuestionId);
            return Result.Fail("Failed to set Answer.");
        }
    }
}
