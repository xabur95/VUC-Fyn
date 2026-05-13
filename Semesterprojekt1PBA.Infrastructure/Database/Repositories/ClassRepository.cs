using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

public class ClassRepository : IClassRepository
{
    public Task CreateClassAsync(Class clas) => throw new NotImplementedException();

    public Task<IEnumerable<Class>> GetAllClassesInSchoolAsync(Guid schoolId) => throw new NotImplementedException();

    public Task<IEnumerable<Class>> GetAllClassesAsync() => throw new NotImplementedException();
}
