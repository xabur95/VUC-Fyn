using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Api;
/// <summary>
/// Author: Michael
/// Centralized exception handler that maps ErrorException domain errors to appropriate HTTP status codes.
/// Registered as middleware so all endpoints benefit without per-endpoint try/catch blocks.
/// </summary>
public class ErrorExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ErrorException errorException)
            return false;

        var statusCode = errorException.ErrorCode switch
        {
            "USER_NOT_FOUND" => StatusCodes.Status404NotFound,
            "ROLE_ALREADY_ASSIGNED" => StatusCodes.Status409Conflict,
            "ROLE_NOT_FOUND" => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status400BadRequest
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = errorException.ErrorCode,
            Detail = errorException.UserMessage ?? errorException.Message
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}