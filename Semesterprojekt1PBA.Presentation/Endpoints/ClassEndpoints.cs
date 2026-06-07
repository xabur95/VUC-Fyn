using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Semesterprojekt1PBA.Application.Dto.Class.Command;
using Semesterprojekt1PBA.Application.Features.Class.Command;

namespace Semesterprojekt1PBA.Presentation.Endpoints;

public static class ClassEndpoints
{
    public static void MapClassEndpoints(this WebApplication app)
    {
        // Create Class
        app.MapPost("/classes", async (IMediator mediator, CreateClassRequest request) =>
        {
            var result = await mediator.Send(new CreateClassCommand(request));
            return Results.Ok(result);
        });
    }
}
