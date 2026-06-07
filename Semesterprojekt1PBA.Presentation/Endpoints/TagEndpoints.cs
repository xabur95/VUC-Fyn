using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Semesterprojekt1PBA.Application.Dto.Tag.Command;
using Semesterprojekt1PBA.Application.Features.Tag.Command;
using Semesterprojekt1PBA.Application.Features.Tag.Query;

namespace Semesterprojekt1PBA.Presentation.Endpoints;

public static class TagEndpoints
{
    public static void MapTagEndpoints(this WebApplication app)
    {
        // Create Tag
        app.MapPost("/tags", async (IMediator mediator, CreateTagRequest request) =>
        {
            var result = await mediator.Send(new CreateTagCommand(request));
            return Results.Ok(result);
        });

        // Update Tag
        app.MapPut("/tags/{id}", async (IMediator mediator, Guid id, UpdateTagRequest request) =>
        {
            var command = new UpdateTagCommand(request with { TagId = id });
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        // Get All Tags
        app.MapGet("/tags", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllTagsQuery());
            return Results.Ok(result);
        });

        // Get Tag By Id
        app.MapGet("/tags/{id}", async (IMediator mediator, Guid id) =>
        {
            var result = await mediator.Send(new GetTagByIdQuery(id));
            return Results.Ok(result);
        });
    }
}
