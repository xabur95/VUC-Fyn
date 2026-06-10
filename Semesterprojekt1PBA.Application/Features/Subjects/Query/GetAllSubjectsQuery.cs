using MediatR;
using Semesterprojekt1PBA.Application.Dto.Subject.Query;

namespace Semesterprojekt1PBA.Application.Features.Subjects.Query;

public record GetAllSubjectsQuery()
    : IRequest<IEnumerable<GetSubjectResponse>>;