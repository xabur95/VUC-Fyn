using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Semesterprojekt1PBA.Application.Dto.Class.Command;
using Semesterprojekt1PBA.Application.Features.Class.Command;
using Semesterprojekt1PBA.Application.Features.Class.Query;
using AddStudentRequest = Semesterprojekt1PBA.Application.Dto.Class.Command.AddStudentRequest;

namespace Semesterprojekt1PBA.Presentation.Endpoints;

public static class ClassEndpoints
{
    public static void MapClassEndpoints(this WebApplication app)
    {
        // Get All Classes
        app.MapGet("/classes", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllClassesQuery());
            return Results.Ok(result);
        });

        // Create Class
        app.MapPost("/classes", async (IMediator mediator, CreateClassRequest request) =>
        {
            var result = await mediator.Send(new CreateClassCommand(request));
            return Results.Ok(result);
        });

        // Add Student to Class
        app.MapPost("/classes/{classId}/students", async (IMediator mediator, Guid classId, AddStudentRequest request) =>
        {
            await mediator.Send(new AddStudentToClassCommand(classId, request.StudentId));
            return Results.Ok();
        });
    }
}
