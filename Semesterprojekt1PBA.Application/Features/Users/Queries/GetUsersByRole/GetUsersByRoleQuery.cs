using MediatR;
using Semesterprojekt1PBA.Application.Dto.Users;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Application.Features.Users.Queries.GetUsersByRole;
/// <summary>
/// Author: Michael
/// Represents a query to retrieve a list of users assigned to a specified role.
/// Use this query with a mediator to obtain all users associated with the given role type. The result
/// contains user information relevant to the specified role.</summary>

public record GetUsersByRoleQuery : IRequest<List<GetUsersByRoleResponse>>
{
    public RoleType RoleType { get; init; }
}