using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

public class EvaluationSheetRepository : IEvaluationSheetRepository
{
    private readonly AppDbContext _appDbContext;

    public EvaluationSheetRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(EvaluationSheet evaluationSheet)
    {
        await _appDbContext.EvaluationSheets.AddAsync(evaluationSheet);
    }

    public Task UpdateAsync(EvaluationSheet evaluationSheet)
    {
        _appDbContext.EvaluationSheets.Update(evaluationSheet);
        return Task.CompletedTask;
    }

    public async Task<EvaluationSheet> GetByIdAsync(Guid id)
    {
        var sheet = await _appDbContext.EvaluationSheets
            .Include(e => e.Class)
                .ThenInclude(c => c.Students)
            .Include(e => e.TeacherScores)
            .Include(e => e.StudentScores)
            .FirstOrDefaultAsync(e => e.Id == id);

        return sheet
            ?? throw new ErrorException($"EvaluationSheet with id '{id}' was not found.", "EVALUATIONSHEET_NOT_FOUND");
    }

    public async Task<IEnumerable<EvaluationSheet>> GetAllAsync()
    {
        return await _appDbContext.EvaluationSheets
            .Include(e => e.Class)
                .ThenInclude(c => c.Students)
            .Include(e => e.TeacherScores)
            .Include(e => e.StudentScores)
            .ToListAsync();
    }

    public async Task<IEnumerable<EvaluationSheet>> GetByClassIdAsync(Guid classId)
    {
        return await _appDbContext.EvaluationSheets
            .Include(e => e.Class)
                .ThenInclude(c => c.Students)
            .Include(e => e.TeacherScores)
            .Include(e => e.StudentScores)
            .Where(e => e.Class.Id == classId)
            .ToListAsync();
    }
}
