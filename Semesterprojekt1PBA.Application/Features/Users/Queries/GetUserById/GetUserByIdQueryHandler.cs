using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Users;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Users.Queries.GetUserById;
/// <summary>
/// Author: Michael
/// Handles queries to retrieve a user by ID and returns the user's information.
/// </summary>
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly ILogger<GetUserByIdQueryHandler> _logger;
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository, ILogger<GetUserByIdQueryHandler> logger)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                throw new ErrorException($"User with id '{request.Id}' was not found.", errorCode: "USER_NOT_FOUND");
            }

            var result = new GetUserByIdResponse
            {
                Id = user.Id,
                FirstName = user.Name.FirstName,
                LastName = user.Name.LastName,
                Email = user.Email.Value,
                Roles = user.Roles.Select(r => r.RoleType)
            };

            return result;
        }
        catch (ErrorException ex)
        {
            _logger.LogError(ex, "Domain error occurred while getting user from database. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}",
                ex.ErrorCode, ex.UserMessage);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while getting user from database.");
            throw;
        }
    }
}