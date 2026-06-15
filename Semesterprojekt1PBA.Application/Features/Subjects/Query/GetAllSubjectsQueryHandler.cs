using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Subject.Query;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;

namespace Semesterprojekt1PBA.Application.Features.Subjects.Query
{
    public class GetAllSubjectsQueryHandler(
        ILogger<GetAllSubjectsQueryHandler> logger,
        ISubjectRepository subjectRepository)
        : IRequestHandler<GetAllSubjectsQuery, IEnumerable<GetSubjectResponse>>
    {
        public async Task<IEnumerable<GetSubjectResponse>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var subjects = await subjectRepository.GetAllSubjectsAsync();

                var responses = subjects.Select(s => new GetSubjectResponse(
                    s.Id,
                    s.RowVersion,
                    s.Title.Value,
                    s.Level));

                return responses;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error fetching all subjects.");
                throw;
            }
        }
    }
}
