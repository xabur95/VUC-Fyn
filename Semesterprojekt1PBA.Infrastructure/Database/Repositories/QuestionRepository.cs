using Microsoft.EntityFrameworkCore;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly AppDbContext _appDbContext;

    public QuestionRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task CreateQuestionAsync(Question question)
    {
        await _appDbContext.Questions.AddAsync(question);
    }

    public Task UpdateQuestionAsync(Question question)
    {
        _appDbContext.Questions.Update(question);
        return Task.CompletedTask;
    }

    public async Task<Question> GetQuestionByIdAsync(Guid id)
    {
        var question = await _appDbContext.Questions
            .Include(q => q.Answer)
            .Include(q => q.Tags)
            .Include(q => q.Subjects)
            .Include(q => q.ParentQuestion)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question is null)
        {
            throw new ErrorException($"Question with id '{id}' was not found.", errorCode: "QUESTION_NOT_FOUND");
        }

        return question;
    }

    public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
    {
        return await _appDbContext.Questions
            .Include(q => q.Answer)
            .Include(q => q.Tags)
            .Include(q => q.Subjects)
            .ToListAsync();
    }

    public async Task<IEnumerable<Question>> GetQuestionsByUserIdAsync(Guid userId)
    {
        return await _appDbContext.Questions
            .Include(q => q.Answer)
            .Include(q => q.Tags)
            .Include(q => q.Subjects)
            .Where(q => q.CreatedByUserId == userId)
            .ToListAsync();
    }
}
