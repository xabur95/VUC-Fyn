using MediatR;
using Semesterprojekt1PBA.Application.Dto.Users;

namespace Semesterprojekt1PBA.Application.Features.Users.Queries.GetUserById;
/// <summary>
/// Author: Michael
/// Forespørgsel til at hente en bruger via ID.
/// Returnerer brugerens oplysninger, hvis fundet.
/// </summary>
public record GetUserByIdQuery : IRequest<GetUserByIdResponse>
{
    public Guid Id { get; init; }
}