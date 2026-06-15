using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Class.Query;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Class.Query;

public class GetAllClassesQueryHandler(
    IClassRepository classRepository,
    ILogger<GetAllClassesQueryHandler> logger)
    : IRequestHandler<GetAllClassesQuery, List<GetAllClassesResponse>>
{
    public async Task<List<GetAllClassesResponse>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var classes = await classRepository.GetAllClassesAsync();

            return classes.Select(c => new GetAllClassesResponse
            {
                Id = c.Id,
                Title = c.Title.Value,
                StartDate = c.ClassDateRange.Start,
                EndDate = c.ClassDateRange.End,
                StudentCount = c.Students.Count
            }).ToList();
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex, "Domain error in GetAllClassesQuery. ErrorCode: {ErrorCode}", ex.ErrorCode);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error in GetAllClassesQuery.");
            throw;
        }
    }
}
