using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Topics.Commands.CreateTopic
{
    public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand, Guid>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ILogger _logger;

        public CreateTopicCommandHandler(ITopicRepository topicRepository, ILogger logger)
        {
            _topicRepository = topicRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Topic topic = Topic.Create(request.Name);

                await _topicRepository.AddAsync(topic);

                return topic.Id;
            }
            catch (ErrorException ex)
            {
                _logger.LogError(ex, "Domain error occurred while creating Topic. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}", ex.ErrorCode, ex.UserMessage);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating topic.");
                throw;
            }
        }
    }
}
