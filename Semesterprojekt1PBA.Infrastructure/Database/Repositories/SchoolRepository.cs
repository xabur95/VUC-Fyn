using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

public class SchoolRepository : ISchoolRepository
{
    public Task CreateSchoolAsync(School school) => throw new NotImplementedException();

    public Task UpdateSchoolTitleAsync(School school) => throw new NotImplementedException();

    public Task<IEnumerable<School>> GetAllSchoolsAsync() => throw new NotImplementedException();

    public Task<School> GetSchoolByIdAsync(Guid id) => throw new NotImplementedException();
}
