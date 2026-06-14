using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Semesterprojekt1PBA.Application.Features.Subjects.Command;
using Semesterprojekt1PBA.Application.Features.Subjects.Query;

namespace Semesterprojekt1PBA.Presentation.Endpoints;

public static class SubjectEndpoints
{
    public static void MapSubjectEndpoints(this WebApplication app)
    {
        // Create Subject
        app.MapPost("/subjects", async (IMediator mediator, CreateSubjectCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        // Get ALL Subjects
        app.MapGet("/subjects", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllSubjectsQuery());
            return Results.Ok(result);
        });
    }
}
