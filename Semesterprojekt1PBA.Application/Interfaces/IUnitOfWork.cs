using System.Data;

namespace Semesterprojekt1PBA.Application.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Serializable);
}