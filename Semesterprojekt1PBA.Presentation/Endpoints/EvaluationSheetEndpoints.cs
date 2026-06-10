using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Semesterprojekt1PBA.Application.Dto.EvaluationSheet.Command;
using Semesterprojekt1PBA.Application.Features.EvaluationSheet.Command;
using Semesterprojekt1PBA.Application.Features.EvaluationSheet.Query;

namespace Semesterprojekt1PBA.Presentation.Endpoints;

public static class EvaluationSheetEndpoints
{
    public static void MapEvaluationSheetEndpoints(this WebApplication app)
    {
        // Create EvaluationSheet
        app.MapPost("/evaluation-sheets", async (IMediator mediator, CreateEvaluationSheetRequest request) =>
        {
            var result = await mediator.Send(new CreateEvaluationSheetCommand(request));
            return Results.Ok(result);
        });

        // Set Teacher Score on EvaluationSheet
        app.MapPut("/evaluation-sheets/{id}/teacher-scores", async (IMediator mediator, Guid id, SetTeacherScoreRequest request) =>
        {
            var command = new SetTeacherScoreCommand(request with { EvaluationSheetId = id });
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        // Set Student Score on EvaluationSheet (self-assessment)
        app.MapPut("/evaluation-sheets/{id}/student-scores", async (IMediator mediator, Guid id, SetStudentScoreRequest request) =>
        {
            var command = new SetStudentScoreCommand(request with { EvaluationSheetId = id });
            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        // Get All EvaluationSheets
        app.MapGet("/evaluation-sheets", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllEvaluationSheetsQuery());
            return Results.Ok(result);
        });

        // Get EvaluationSheet By Id
        app.MapGet("/evaluation-sheets/{id}", async (IMediator mediator, Guid id) =>
        {
            var result = await mediator.Send(new GetEvaluationSheetByIdQuery(id));
            return Results.Ok(result);
        });

        // Get EvaluationSheets By Class Id
        app.MapGet("/classes/{classId}/evaluation-sheets", async (IMediator mediator, Guid classId) =>
        {
            var result = await mediator.Send(new GetEvaluationSheetsByClassIdQuery(classId));
            return Results.Ok(result);
        });
    }
}
