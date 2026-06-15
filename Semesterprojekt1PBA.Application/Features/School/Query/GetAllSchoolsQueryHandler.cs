using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.School.Query;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.School.Query;

public class GetAllSchoolsQueryHandler : IRequestHandler<GetAllSchoolsQuery, List<GetDetailedSchoolResponse>>
{
    private readonly ILogger<GetAllSchoolsQueryHandler> _logger;
    private readonly ISchoolRepository _schoolRepository;

    public GetAllSchoolsQueryHandler(ISchoolRepository schoolRepository, ILogger<GetAllSchoolsQueryHandler> logger)
    {
        _schoolRepository = schoolRepository;
        _logger = logger;
    }

    public async Task<List<GetDetailedSchoolResponse>> Handle(GetAllSchoolsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var schools = await _schoolRepository.GetAllSchoolsAsync();

            return schools.Select(s => new GetDetailedSchoolResponse(
                s.Id,
                s.RowVersion,
                s.Title.Value,
                s.Classes.Select(c => new GetSchoolClassResponse(c.Id))
            )).ToList();
        }
        catch (ErrorException ex)
        {
            _logger.LogError(ex, "Domain error occurred while getting all schools. ErrorCode: {ErrorCode}", ex.ErrorCode);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while getting all schools.");
            throw;
        }
    }
}
