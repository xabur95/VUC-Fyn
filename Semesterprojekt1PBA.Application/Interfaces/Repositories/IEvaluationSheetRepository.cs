using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Application.Interfaces.Repositories
{
    public interface IEvaluationSheetRepository
    {
        Task AddAsync(EvaluationSheet evaluationSheet);
        Task UpdateAsync(EvaluationSheet evaluationSheet);
        Task<EvaluationSheet> GetByIdAsync(Guid id);
        Task<IEnumerable<EvaluationSheet>> GetAllAsync();
        Task<IEnumerable<EvaluationSheet>> GetByClassIdAsync(Guid classId);
    }
}
