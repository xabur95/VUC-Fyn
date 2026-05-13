using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Application.Interfaces.Repositories;

public interface ITagRepository
{
    Task<Tag> GetTagByIdAsync(Guid id);
    Task<IEnumerable<Tag>> GetTagsByIdsAsync(IEnumerable<Guid> ids);
    Task<IEnumerable<Tag>> GetAllTagsAsync();
    Task CreateTagAsync(Tag tag);
    Task UpdateTagAsync(Tag tag);
}
