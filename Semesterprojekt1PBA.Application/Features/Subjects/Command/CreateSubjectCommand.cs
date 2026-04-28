using MediatR;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Application.Features.Subjects.Command
{
    public record CreateSubjectCommand : IRequest<Guid>
    {
        public string Name { get; init; } = null!;
        public Level Level { get; init; }
    }
}
