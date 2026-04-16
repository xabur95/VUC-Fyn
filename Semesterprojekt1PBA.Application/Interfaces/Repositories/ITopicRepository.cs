using Semesterprojekt1PBA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Application.Interfaces.Repositories
{
    public interface ITopicRepository
    {
        Task AddAsync(Topic topic);
        Task<IReadOnlyCollection<Topic>> GetTopicsBySubjectAsync(Subject subject);
        Task UpdateAsync(Topic topic);
    }
}
