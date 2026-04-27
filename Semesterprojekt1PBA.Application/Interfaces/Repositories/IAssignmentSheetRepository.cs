using Semesterprojekt1PBA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Application.Interfaces.Repositories
{
    public interface IAssignmentSheetRepository
    {
        Task AddAsync(AssignmentSheet assignmentSheet);
        Task<List<AssignmentSheet>> GetAllAsync();
        Task UpdateAsync (AssignmentSheet assignmentSheet);
    }
}
