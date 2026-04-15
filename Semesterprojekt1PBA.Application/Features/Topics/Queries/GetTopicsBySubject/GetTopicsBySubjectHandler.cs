using MediatR;
using Semesterprojekt1PBA.Application.Dto.Topics;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Topics.Queries.GetTopicsBySubject
{
    public class GetTopicsBySubjectHandler : IRequestHandler<GetTopicsBySubjectQuery, IReadOnlyCollection<GetTopicsBySubjectResponse>>
    {

        private readonly ITopicRepository _topicRepository;

        public GetTopicsBySubjectHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<IReadOnlyCollection<GetTopicsBySubjectResponse>> Handle(GetTopicsBySubjectQuery request, CancellationToken cancellationToken)
        {
            var topics = await _topicRepository.GetTopicsBySubjectAsync(request.subject);

            return topics.Select(topic => new GetTopicsBySubjectResponse
                 {
                     Name = topic.Name,
                 }).ToList();
        }
    }
}
