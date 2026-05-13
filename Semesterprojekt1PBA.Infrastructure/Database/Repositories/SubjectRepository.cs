using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

public class SubjectRepository : ISubjectRepository
{
    public Task AddAsync(Subject subject) => throw new NotImplementedException();

    public Task<IReadOnlyCollection<Subject>> GetByNameAsyc(string name) => throw new NotImplementedException();
}
