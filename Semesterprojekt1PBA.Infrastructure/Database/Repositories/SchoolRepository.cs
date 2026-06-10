using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

public class SchoolRepository : ISchoolRepository
{
    private readonly AppDbContext _appDbContext;

    public SchoolRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task CreateSchoolAsync(School school)
    {
        await _appDbContext.Schools.AddAsync(school);
    }

    public Task UpdateSchoolTitleAsync(School school)
    {
        _appDbContext.Schools.Update(school);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<School>> GetAllSchoolsAsync()
    {
        return await _appDbContext.Schools
            .Include(s => s.Classes)
            .Include(s => s.SchoolResponsibles)
            .ToListAsync();
    }

    public async Task<School> GetSchoolByIdAsync(Guid id)
    {
        var school = await _appDbContext.Schools
            .Include(s => s.Classes)
            .Include(s => s.SchoolResponsibles)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (school is null)
        {
            throw new ErrorException($"School with id '{id}' was not found.", errorCode: "SCHOOL_NOT_FOUND");
        }

        return school;
    }
}
