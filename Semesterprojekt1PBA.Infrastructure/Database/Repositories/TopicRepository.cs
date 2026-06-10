using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

public class TopicRepository : ITopicRepository
{
    private readonly AppDbContext _appDbContext;

    public TopicRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(Topic topic)
    {
        await _appDbContext.Topics.AddAsync(topic);
    }

    public async Task<IReadOnlyCollection<Topic>> GetTopicsBySubjectAsync(Guid subjectId)
    {
        var subject = await _appDbContext.Subjects
            .Include(s => s.Topics)
            .FirstOrDefaultAsync(s => s.Id == subjectId);

        if (subject is null)
        {
            throw new ErrorException($"Subject with id '{subjectId}' was not found.", errorCode: "SUBJECT_NOT_FOUND");
        }

        return subject.Topics.ToList().AsReadOnly();
    }

    public Task UpdateAsync(Topic topic)
    {
        _appDbContext.Topics.Update(topic);
        return Task.CompletedTask;
    }
}
