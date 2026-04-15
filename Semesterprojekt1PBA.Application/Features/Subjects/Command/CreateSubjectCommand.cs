using MediatR;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Features.Subjects.Command
{
    public record CreateSubjectCommand : IRequest<Guid>
    {
        public string Name { get; init; } = null!;
        public Level Level { get; init; }
    }
}
