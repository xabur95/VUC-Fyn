using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Semesterprojekt1PBA.Application.Dto.School.Command;
using Semesterprojekt1PBA.Application.Features.School.Command;
using Semesterprojekt1PBA.Application.Features.School.Query;
using GetAllSchoolsQuery = Semesterprojekt1PBA.Application.Features.School.Query.GetAllSchoolsQuery;

namespace Semesterprojekt1PBA.Presentation.Endpoints;

public static class SchoolEndpoints
{
    public static void MapSchoolEndpoints(this WebApplication app)
    {
        // Get All Schools
        app.MapGet("/schools", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllSchoolsQuery());
            return Results.Ok(result);
        });

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
