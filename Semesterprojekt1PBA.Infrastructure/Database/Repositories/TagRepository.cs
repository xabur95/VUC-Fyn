using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

public class TagRepository : ITagRepository
{
    private readonly AppDbContext _appDbContext;

    public TagRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Tag> GetTagByIdAsync(Guid id)
    {
        var tag = await _appDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);

        if (tag is null)
        {
            throw new ErrorException($"Tag with id '{id}' was not found.", errorCode: "TAG_NOT_FOUND");
        }

        return tag;
    }

    public async Task<IEnumerable<Tag>> GetTagsByIdsAsync(IEnumerable<Guid> ids)
    {
        var idList = ids.ToList();
        var tags = await _appDbContext.Tags.Where(t => idList.Contains(t.Id)).ToListAsync();
        return tags;
    }

    public async Task<IEnumerable<Tag>> GetAllTagsAsync()
    {
        return await _appDbContext.Tags.ToListAsync();
    }

    public async Task CreateTagAsync(Tag tag)
    {
        await _appDbContext.Tags.AddAsync(tag);
    }

    public Task UpdateTagAsync(Tag tag)
    {
        _appDbContext.Tags.Update(tag);
        return Task.CompletedTask;
    }
}
