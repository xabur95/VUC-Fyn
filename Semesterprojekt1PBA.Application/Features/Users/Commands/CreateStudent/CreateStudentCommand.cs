using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.CreateStudent;

public record CreateStudentCommand(string FirstName, string LastName, string Email, string Password, string Knr, DateOnly Tilmeldt, DateOnly? Ophørt) : IRequest<Guid>, ITransactionalCommand;