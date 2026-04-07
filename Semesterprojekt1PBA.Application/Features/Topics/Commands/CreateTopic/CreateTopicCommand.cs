using MediatR;

namespace Semesterprojekt1PBA.Application.Features.Topics.Commands.CreateTopic
{
    public record CreateTopicCommand : IRequest<Guid>
    {
        public string Name { get; set; } = null!;
    }
}
