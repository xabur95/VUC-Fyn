using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.CreateTeacher;

public record CreateTeacherCommand(string FirstName, string LastName, string Email, string Password) : IRequest<Guid>, ITransactionalCommand;