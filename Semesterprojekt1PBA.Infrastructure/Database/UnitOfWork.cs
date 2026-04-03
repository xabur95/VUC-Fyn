using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Semesterprojekt1PBA.Application.Interfaces;

namespace Semesterprojekt1PBA.Infrastructure.Database
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private readonly DbContext _database;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(TDbContext database)
        {
            _database = database;
        }
        async Task IUnitOfWork.BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            if (_database.Database.CurrentTransaction != null) return;
            _transaction = await _database.Database.BeginTransactionAsync(isolationLevel);
        }

        async Task IUnitOfWork.CommitAsync()
        {
            if (_transaction == null) throw new Exception("You must call 'BeginTransaction' before Commit is called");
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
        }

        async Task IUnitOfWork.RollbackAsync()
        {
            if (_transaction == null) throw new Exception("You must call 'BeginTransaction' before Rollback is called");
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }
}
