using MediatR;
using Semesterprojekt1PBA.Application.Dto.Topics;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;

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
            var topics = await _topicRepository.GetTopicsBySubjectAsync(request.Subject);

            return topics.Select(topic => new GetTopicsBySubjectResponse(topic.Name)).ToList();
        }
    }
}
