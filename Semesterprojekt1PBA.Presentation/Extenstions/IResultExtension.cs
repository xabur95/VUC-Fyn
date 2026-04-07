using FluentResults;
using Microsoft.AspNetCore.Http;

namespace Semesterprojekt1PBA.Presentation.Extenstions
{
    public static class IResultExtension
    {
        public static IResult ReturnHttpResult<TResponse>(this Result<TResponse> result)
        {
            if (result.IsSuccess)
                return Results.Ok(result.Value);

            var messages = result.Errors.Select(e => e.Message).ToArray();
            return Results.BadRequest(new { errors = messages });
        }
    }
}


Jeg skal lave:
    Class Queries;
Endpoints og en måde man kan kommunikere med applikationen ude fra 
    