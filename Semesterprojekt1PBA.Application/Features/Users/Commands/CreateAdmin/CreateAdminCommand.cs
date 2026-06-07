using MediatR;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.CreateAdmin;

public record CreateAdminCommand(string FirstName, string LastName, string Email, string Password) : IRequest<Guid>;