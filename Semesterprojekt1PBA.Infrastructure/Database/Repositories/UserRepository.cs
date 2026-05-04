using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;
/// <summary>
/// Author: Michael
/// The UserRepository implements the IUserRepository interface to encapsulate data access logic for
/// users. It supports asynchronous operations for adding, updating, and retrieving users, including filtering by user
/// roles. All methods interact with the underlying AppDbContext and are intended for use in application-level data
/// management scenarios.</summary>
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(User user)
    {
        await _appDbContext.Users.AddAsync(user);
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await _appDbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == id && u.IsActive);

        if (user is null)
        {
            throw new ErrorException($"User with id '{id}' was not found.", errorCode: "USER_NOT_FOUND");
        }

        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _appDbContext.Users.Update(user);
    }

    public async Task<List<User>> GetByRoleAsync(RoleType roleType)
    {
        var users = _appDbContext.Users.Include(u => u.Roles).Where(u => u.Roles.Any(r => r.RoleType == roleType) && u.IsActive);

        return await users.ToListAsync();
    }
}