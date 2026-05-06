using Semesterprojekt1PBA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Application.Interfaces.Repositories
{
    public interface ISubjectRepository
    {
        Task AddAsync(Subject subject);
        Task<IReadOnlyCollection<Subject>> GetByNameAsyc(string name);

    }
}
