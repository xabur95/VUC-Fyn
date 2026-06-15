using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Users;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Application.Features.Users.Queries.GetUsersByRole;
/// <summary>
/// Author: Michael
/// Handles the GetUsersByRoleQuery and returns user information for all users with the specified role.
/// Depends on IUserRepository for data access.
/// </summary>
public class GetUsersByRoleQueryHandler : IRequestHandler<GetUsersByRoleQuery, List<GetUsersByRoleResponse>>
{
    private readonly ILogger<GetUsersByRoleQueryHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IClassRepository _classRepository;

    public GetUsersByRoleQueryHandler(
        IUserRepository userRepository,
        IClassRepository classRepository,
        ILogger<GetUsersByRoleQueryHandler> logger)
    {
        _logger = logger;
        _userRepository = userRepository;
        _classRepository = classRepository;
    }

    public async Task<List<GetUsersByRoleResponse>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userRepository.GetByRoleAsync(request.RoleType);

            // Byg opslag: userId → liste af klassenavne (kun relevant for studerende)
            Dictionary<Guid, List<string>> classesPerUser = [];
            if (request.RoleType == RoleType.Student)
            {
                var allClasses = await _classRepository.GetAllClassesAsync();
                foreach (var c in allClasses)
                    foreach (var student in c.Students)
                    {
                        if (!classesPerUser.TryGetValue(student.Id, out var list))
                        {
                            list = [];
                            classesPerUser[student.Id] = list;
                        }
                        list.Add(c.Title.Value);
                    }
            }

            var result = users.Select(u => new GetUsersByRoleResponse
            {
                Id = u.Id,
                FirstName = u.Name.FirstName,
                LastName = u.Name.LastName,
                Email = u.Email.Value,
                IsActive = u.IsActive,
                Classes = classesPerUser.TryGetValue(u.Id, out var classes) ? classes : []
            }).ToList();

            return result;
        }
        catch (ErrorException ex)
        {
            _logger.LogError(ex, "Domain error occurred while getting users by role. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}",
                ex.ErrorCode, ex.UserMessage);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while getting users by role.");
            throw;
        }
    }
}
