using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Semesterprojekt1PBA.Application.Dto.Answer.Command;
using Semesterprojekt1PBA.Application.Features.Answer.Command;

namespace Semesterprojekt1PBA.Presentation.Endpoints;

public static class AnswerEndpoints
{
    public static void MapAnswerEndpoints(this WebApplication app)
    {
        // Set Answer on Question
        app.MapPost("/questions/{questionId}/answer", async (IMediator mediator, Guid questionId, SetAnswerRequest request) =>
        {
            var command = new SetAnswerCommand(request with { QuestionId = questionId });
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        // Update Answer on Question
        app.MapPut("/questions/{questionId}/answer", async (IMediator mediator, Guid questionId, UpdateAnswerRequest request) =>
        {
            var command = new UpdateAnswerCommand(request with { QuestionId = questionId });
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        // Remove Answer from Question
        app.MapDelete("/questions/{questionId}/answer", async (IMediator mediator, Guid questionId, [FromBody] RemoveAnswerRequest request) =>
        {
            var command = new RemoveAnswerCommand(request with { QuestionId = questionId });
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });
    }
}
