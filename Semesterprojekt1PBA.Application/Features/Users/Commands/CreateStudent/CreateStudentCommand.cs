using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.CreateStudent;
/// <summary>
/// Author: Michael
/// Represents a command to create a new student with the specified details.
/// This command is typically used in a CQRS pattern to encapsulate the data required for creating a
/// student entity. The command includes personal information and enrollment dates. Implements transactional semantics
/// to ensure data consistency when processed.
/// </summary>
public record CreateStudentCommand : IRequest<Guid>, ITransactionalCommand
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Knr { get; init; }
    public DateOnly Tilmeldt { get; init; }
    public DateOnly? Ophørt { get; init; }
}