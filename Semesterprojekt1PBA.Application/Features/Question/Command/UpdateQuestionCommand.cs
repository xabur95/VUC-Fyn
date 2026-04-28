using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Question.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Question.Command;

public record UpdateQuestionCommand(UpdateQuestionRequest UpdateQuestionRequest)
    : IRequest<Result<bool>>, ITransactionalCommand;

public class UpdateQuestionCommandHandler(
    ILogger logger,
    IQuestionRepository questionRepository,
    IUserRepository userRepository)
    : IRequestHandler<UpdateQuestionCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var req = request.UpdateQuestionRequest;

            // Load
            var question = await questionRepository.GetQuestionByIdAsync(req.QuestionId);
            var editor = await userRepository.GetByIdAsync(req.EditorUserId);

            //TODO: if the question is being used in any AssignmentSheet, then it throws and ErrorEXception 


            // Do — domain enforces ownership
            question.Update(
                editor,
                req.Title,
                req.Text,
                req.Points,
                req.ActiveStatus);

            // Save
            await questionRepository.UpdateQuestionAsync(question);

            return Result.Ok().WithSuccess("Question updated successfully.");
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex, "Domain error occurred while updating the question. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}", ex.ErrorCode, ex.UserMessage);
            return Result.Fail(ex.UserMessage ?? "Failed to update Question due to a domain error.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating question with id {Id}", request.UpdateQuestionRequest.QuestionId);
            return Result.Fail("Something went wrong while updating the question.");
        }
    }
}
