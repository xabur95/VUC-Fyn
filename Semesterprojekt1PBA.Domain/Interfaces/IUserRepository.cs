using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Domain.Interfaces;
/// <summary>
/// Author: Michael
/// Definerer metoder til at gemme og hente brugere fra en datakilde.
/// Implementeringer håndterer persistens og adgang til brugerdata.
/// </summary>
public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User> GetByIdAsync(Guid id);
}