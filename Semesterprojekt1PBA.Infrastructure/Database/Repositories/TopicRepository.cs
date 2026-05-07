using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Interfaces;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

public class TopicRepository : ITopicRepository
{
    public Task AddAsync(Topic topic) => throw new NotImplementedException();

    public Task<IReadOnlyCollection<Topic>> GetTopicsBySubjectAsync(Subject subject) => throw new NotImplementedException();

    public Task UpdateAsync(Topic topic) => throw new NotImplementedException();
}
