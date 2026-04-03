using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Semesterprojekt1PBA.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task RollbackAsync();
        Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Serializable);
    }
}
