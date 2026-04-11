using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Interfaces;
/// <summary>
/// Author: Michael
/// Defines methods for saving and retrieving users from a data source.
/// Implementations handle persistence and access to user data.
/// </summary>
public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User> GetByIdAsync(Guid id);
    Task UpdateAsync(User user);
    Task<List<User>> GetByRoleAsync(RoleType roleType);
}