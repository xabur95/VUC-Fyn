using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly AppDbContext _appDbContext;

    public SubjectRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(Subject subject)
    {
        await _appDbContext.Subjects.AddAsync(subject);
    }

    public async Task<IReadOnlyCollection<Subject>> GetByNameAsync(string name)
    {
        var subjects = await _appDbContext.Subjects
            .Include(s => s.Topics)
            .Where(s => s.Title.Value == name)
            .ToListAsync();

        return subjects.AsReadOnly();
    }

    public async Task<IReadOnlyCollection<Subject>> GetAllSubjectsAsync()
    {
        var subjects = await _appDbContext.Subjects
            .Include(s => s.Topics)
            .ToListAsync();

        return subjects.AsReadOnly();
    }
}
