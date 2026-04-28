using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Application.Interfaces.Repositories;

public interface IQuestionRepository
{
    Task CreateQuestionAsync(Question question);
    Task UpdateQuestionAsync(Question question);
    Task<Question> GetQuestionByIdAsync(Guid id);
    Task<IEnumerable<Question>> GetAllQuestionsAsync();
    Task<IEnumerable<Question>> GetQuestionsByUserIdAsync(Guid userId);
}
