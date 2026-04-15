using MediatR;
using Semesterprojekt1PBA.Application.Dto.Topics;
using Semesterprojekt1PBA.Domain.Entities;


namespace Semesterprojekt1PBA.Application.Features.Topics.Queries.GetTopicsBySubject
{
    public record GetTopicsBySubjectQuery : IRequest<IReadOnlyCollection<GetTopicsBySubjectResponse>>
    {
        public Subject subject { get; set; }
    }
}
