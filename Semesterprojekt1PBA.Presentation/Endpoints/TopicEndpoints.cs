using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Semesterprojekt1PBA.Application.Features.Topics.Commands.CreateTopic;
using Semesterprojekt1PBA.Application.Features.Topics.Queries.GetTopicsBySubject;

namespace Semesterprojekt1PBA.Presentation.Endpoints;

public static class TopicEndpoints
{
    public static void MapTopicEndpoints(this WebApplication app)
    {
        // Create Topic
        app.MapPost("/topics", async (IMediator mediator, CreateTopicCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        // Get Topics By Subject
        app.MapGet("/subjects/{subjectId}/topics", async (IMediator mediator, Guid subjectId) =>
        {
            var result = await mediator.Send(new GetTopicsBySubjectQuery { Subject = subjectId });
            return Results.Ok(result);
        });
    }
}
