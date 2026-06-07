using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Semesterprojekt1PBA.Application.Dto.School.Command;
using Semesterprojekt1PBA.Application.Features.School.Command;
using Semesterprojekt1PBA.Application.Features.School.Query;

namespace Semesterprojekt1PBA.Presentation.Endpoints;

public static class SchoolEndpoints
{
    public static void MapSchoolEndpoints(this WebApplication app)
    {
        // Create School
        app.MapPost("/schools", async (IMediator mediator, CreateSchoolRequest request) =>
        {
            var result = await mediator.Send(new CreateSchoolCommand(request));
            return Results.Ok(result);
        });

        // Update School Title
        app.MapPut("/schools/{id}", async (IMediator mediator, Guid id, UpdateSchoolTitleRequest request) =>
        {
            var command = new UpdateSchoolTitleCommand(request with { Id = id });
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        // Get School By Id
        app.MapGet("/schools/{id}", async (IMediator mediator, Guid id) =>
        {
            var result = await mediator.Send(new GetSchoolQuery(id));
            return Results.Ok(result);
        });
    }
}
