using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Semesterprojekt1PBA.Application.Features.AssignmentSheets.Command.CreateAssignmentSheet;
using Semesterprojekt1PBA.Application.Features.AssignmentSheets.Query.GetAssignmentSheetById;

namespace Semesterprojekt1PBA.Presentation.Endpoints;

public static class AssignmentSheetEndpoints
{
    public static void MapAssignmentSheetEndpoints(this WebApplication app)
    {
        // Create AssignmentSheet
        app.MapPost("/assignment-sheets", async (IMediator mediator, CreateAssignmentSheetCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        // Get AssignmentSheet By Id
        app.MapGet("/assignment-sheets/{id}", async (IMediator mediator, Guid id) =>
        {
            var result = await mediator.Send(new GetAssignmentSheetByIdQuery(id));
            return Results.Ok(result);
        });
    }
}
