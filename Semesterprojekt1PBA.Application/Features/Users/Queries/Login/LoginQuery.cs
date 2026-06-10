using MediatR;
using Semesterprojekt1PBA.Application.Dto.Users;

namespace Semesterprojekt1PBA.Application.Features.Users.Queries.Login;

public record LoginQuery : IRequest<LoginResponse>
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}
