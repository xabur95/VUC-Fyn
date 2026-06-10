using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

public class AssignmentSheetRepository : IAssignmentSheetRepository
{
    private readonly AppDbContext _appDbContext;

    public AssignmentSheetRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(AssignmentSheet assignmentSheet)
    {
        await _appDbContext.AssignmentSheets.AddAsync(assignmentSheet);
    }

    public async Task<AssignmentSheet> GetByIdAsync(Guid id)
    {
        var assignmentSheet = await _appDbContext.AssignmentSheets
            .Include(a => a.Author)
            .Include(a => a.Subject)
            .Include(a => a.Topics)
            .Include(a => a.Questions)
            .FirstOrDefaultAsync(a => a.Id == id);

        return assignmentSheet
            ?? throw new ErrorException($"AssignmentSheet with id '{id}' was not found.", "ASSIGNMENTSHEET_NOT_FOUND");
    }

    public async Task<List<AssignmentSheet>> GetAllAsync()
    {
        return await _appDbContext.AssignmentSheets
            .Include(a => a.Author)
            .Include(a => a.Subject)
            .Include(a => a.Topics)
            .Include(a => a.Questions)
            .ToListAsync();
    }

    public Task UpdateAsync(AssignmentSheet assignmentSheet)
    {
        _appDbContext.AssignmentSheets.Update(assignmentSheet);
        return Task.CompletedTask;
    }
}
