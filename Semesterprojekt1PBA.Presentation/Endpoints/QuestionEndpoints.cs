using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Semesterprojekt1PBA.Application.Dto.Question.Command;
using Semesterprojekt1PBA.Application.Features.Question.Command;
using Semesterprojekt1PBA.Application.Features.Question.Query;

namespace Semesterprojekt1PBA.Presentation.Endpoints;

public static class QuestionEndpoints
{
    public static void MapQuestionEndpoints(this WebApplication app)
    {
        // Create Question
        app.MapPost("/questions", async (IMediator mediator, CreateQuestionRequest request) =>
        {
            var result = await mediator.Send(new CreateQuestionCommand(request));
            return Results.Ok(result);
        });

        // Update Question
        app.MapPut("/questions/{id}", async (IMediator mediator, Guid id, UpdateQuestionRequest request) =>
        {
            var command = new UpdateQuestionCommand(request with { QuestionId = id });
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

    
        // Get All Questions
        app.MapGet("/questions", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllQuestionsQuery());
            return Results.Ok(result);
        });

        // Get Question By Id
        app.MapGet("/questions/{id}", async (IMediator mediator, Guid id) =>
        {
            var result = await mediator.Send(new GetQuestionByIdQuery(id));
            return Results.Ok(result);
        });
    }
}
