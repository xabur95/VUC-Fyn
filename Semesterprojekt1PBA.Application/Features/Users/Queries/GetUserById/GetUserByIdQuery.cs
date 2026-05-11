using MediatR;
using Semesterprojekt1PBA.Application.Dto.Users;

namespace Semesterprojekt1PBA.Application.Features.Users.Queries.GetUserById;
/// <summary>
/// Author: Michael
/// Represents a query to retrieve a user by their unique identifier.
/// Use this query with a mediator to request user details for a specific user identified by the provided
/// ID. The response contains the user's information if found.</summary>
public record GetUserByIdQuery : IRequest<GetUserByIdResponse>
{
    public Guid Id { get; init; }
}