using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

public class TopicRepository : ITopicRepository
{
    public Task AddAsync(Topic topic) => throw new NotImplementedException();

    public Task<IReadOnlyCollection<Topic>> GetTopicsBySubjectAsync(Guid subject)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Topic topic) => throw new NotImplementedException();
}
