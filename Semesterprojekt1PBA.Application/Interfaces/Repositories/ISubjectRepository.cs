using Semesterprojekt1PBA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Application.Interfaces.Repositories
{
    public interface ISubjectRepository
    {
        Task AddAsync(Subject subject);
        Task<IEnumerable<Subject>> GetByNameAsync(string name);
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();
        Task<Subject> GetByIdAsync(Guid id);

    }
}
