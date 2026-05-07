using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Semesterprojekt1PBA.Application.Features.Users.Commands.AssignRole;
using Semesterprojekt1PBA.Application.Features.Users.Commands.CreateAdmin;
using Semesterprojekt1PBA.Application.Features.Users.Commands.CreateStudent;
using Semesterprojekt1PBA.Application.Features.Users.Commands.CreateTeacher;
using Semesterprojekt1PBA.Application.Features.Users.Commands.DeactivateUser;
using Semesterprojekt1PBA.Application.Features.Users.Commands.RevokeRole;
using Semesterprojekt1PBA.Application.Features.Users.Commands.UpdateUser;
using Semesterprojekt1PBA.Application.Features.Users.Queries.GetUserById;
using Semesterprojekt1PBA.Application.Features.Users.Queries.GetUsersByRole;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Presentation.Endpoints;
/// <summary>
/// Author: Michael
/// Provides extension methods for mapping user-related API endpoints to a WebApplication instance.
/// This class defines endpoints for creating, updating, deactivating, and retrieving users, as well as
/// assigning and revoking user roles. The endpoints are intended to be registered during application startup to enable
/// user management functionality in the API.
/// </summary>
public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        // Create Admin
        app.MapPost("/users/admin", async (IMediator mediator, CreateAdminCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        // Create Student
        app.MapPost("/users/student", async (IMediator mediator, CreateStudentCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        // Create Teacher
        app.MapPost("/users/teacher", async (IMediator mediator, CreateTeacherCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        // Update User
        app.MapPut("/users/{id}", async (IMediator mediator, Guid id, UpdateUserCommand request) =>
        {
            var command = request with { Id = id };
            await mediator.Send(command);
            return Results.Ok();
        });


        // Deactivate User
        app.MapDelete("/users/{id}/deactivate", async (IMediator mediator, Guid id) =>
        {
            await mediator.Send(new DeactivateUserCommand { Id = id });
            return Results.Ok();
        });

        // Assign Role
        app.MapPost("/users/{id}/roles", async (IMediator mediator, Guid id, AssignRoleCommand request) =>
        {
            var command = request with { Id = id };
            await mediator.Send(command);
            return Results.Ok();
        });

        // Revoke Role
        app.MapDelete("/users/{id}/roles/{roleType}", async (IMediator mediator, Guid id, RoleType roleType) =>
        {
            await mediator.Send(new RevokeRoleCommand { Id = id, RoleType = roleType });
            return Results.Ok();
        });

        // Get User By Id
        app.MapGet("/users/{id}", async (IMediator mediator, Guid id) =>
        {
            var result = await mediator.Send(new GetUserByIdQuery { Id = id });
            return Results.Ok(result);
        });

        // Get Users By Role
        app.MapGet("/users/role/{roleType}", async (IMediator mediator, RoleType roleType) =>
        {
            var result = await mediator.Send(new GetUsersByRoleQuery { RoleType = roleType });
            return Results.Ok(result);
        });
    }
}