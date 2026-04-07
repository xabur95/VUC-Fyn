using Semesterprojekt1PBA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Application.Interfaces.Repositories
{
    public interface ISchoolRepository
    {
        Task CreateSchoolAsync(School school);
        Task UpdateSchoolTitleAsync(School school);
        Task <IEnumerable<School>> GetAllSchoolsAsync();
        Task<School> GetSchoolByIdAsync(Guid id);
    }
}
