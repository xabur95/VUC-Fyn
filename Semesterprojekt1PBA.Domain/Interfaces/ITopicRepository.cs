using Semesterprojekt1PBA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Domain.Interfaces
{
    public interface ITopicRepository
    {
        Task AddAsync(Topic topic);
        Task<IReadOnlyCollection<Topic>> GetTopicsBySubject(Subject subject);
        Task UpdateAsync(Topic topic);
    }
}
