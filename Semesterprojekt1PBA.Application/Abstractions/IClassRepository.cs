using Semesterprojekt1PBA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Application.Abstractions
{
    public interface IClassRepository
    {
        Task CreateClassAsync(Class clas);

        Task<IEnumerable<Class>> GetAllClassesInSchoolAsync(Guid schoolId);
        Task<IEnumerable<Class>> GetAllClassesAsync();
    }
}
