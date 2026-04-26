using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

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

        await _appDbContext.SaveChangesAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await _appDbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
        {
            throw new ErrorException($"User with {id} not found", errorCode: "USER_NOT_FOUND");
        }

        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _appDbContext.Users.Update(user);

        await _appDbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetByRoleAsync(RoleType roleType)
    {
        var users = _appDbContext.Users.Include(u => u.Roles).Where(u => u.Roles.Any(r => r.RoleType == roleType));

        return await users.ToListAsync();
    }
}