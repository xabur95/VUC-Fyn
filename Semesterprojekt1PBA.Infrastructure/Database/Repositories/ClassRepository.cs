using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Semesterprojekt1PBA.Infrastructure.Database.Repositories;

/// <summary>
/// Author: Xabur
/// The ClassRepository implements the IClassRepository interface to encapsulate data access logic for
/// classes. It supports asynchronous operations for adding and retrieving classes, including filtering by school.
/// All methods interact with the underlying AppDbContext and are intended for use in application-level data
/// management scenarios.
/// </summary>
public class ClassRepository : IClassRepository
{
    private readonly AppDbContext _appDbContext;

    public ClassRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task CreateClassAsync(Class clas)
    {
        await _appDbContext.Classes.AddAsync(clas);
    }

    public async Task<Class> GetClassByIdAsync(Guid id)
    {
        var clas = await _appDbContext.Classes
            .Include(c => c.Teachers)
            .Include(c => c.Students)
            .Include(c => c.Subjects)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (clas is null)
        {
            throw new ErrorException($"Class with id '{id}' was not found.", errorCode: "CLASS_NOT_FOUND");
        }

        return clas;
    }

    public async Task<IEnumerable<Class>> GetAllClassesInSchoolAsync(Guid schoolId)
    {
        return await _appDbContext.Classes
            .Where(c => EF.Property<Guid>(c, "SchoolId") == schoolId)
            .Include(c => c.Teachers)
            .Include(c => c.Students)
            .Include(c => c.Subjects)
            .ToListAsync();
    }

    public async Task AddStudentToClassAsync(Guid classId, Guid studentId)
    {
        var clas = await GetClassByIdAsync(classId);

        var student = await _appDbContext.Users.OfType<Student>()
            .FirstOrDefaultAsync(s => s.Id == studentId);

        if (student is null)
            throw new ErrorException($"Student with id '{studentId}' was not found.", errorCode: "USER_NOT_FOUND");

        clas.AddStudent(student);
    }

    public async Task<IEnumerable<Class>> GetAllClassesAsync()
    {
        return await _appDbContext.Classes
            .Include(c => c.Teachers)
            .Include(c => c.Students)
            .Include(c => c.Subjects)
            .ToListAsync();
    }
}
