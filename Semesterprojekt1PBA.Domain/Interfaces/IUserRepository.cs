using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Domain.Interfaces;

public interface IUserRepository
{
    User GetById(Guid id);
}