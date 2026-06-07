using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Question.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Question.Command;

public record UpdateQuestionCommand(UpdateQuestionRequest UpdateQuestionRequest)
    : IRequest<bool>, ITransactionalCommand;

public class UpdateQuestionCommandHandler(
    ILogger<UpdateQuestionCommandHandler> logger,
    IQuestionRepository questionRepository,
    IUserRepository userRepository)
    : IRequestHandler<UpdateQuestionCommand, bool>
{
    public async Task<bool> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var req = request.UpdateQuestionRequest;

            // Load
            var question = await questionRepository.GetQuestionByIdAsync(req.QuestionId);
            var editor = await userRepository.GetByIdAsync<Teacher>(req.EditorUserId);

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

            return true;
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex, "Domain error occurred while updating the question. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}", ex.ErrorCode, ex.UserMessage);
            throw;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating question with id {Id}", request.UpdateQuestionRequest.QuestionId);
            throw;
        }
    }
}
