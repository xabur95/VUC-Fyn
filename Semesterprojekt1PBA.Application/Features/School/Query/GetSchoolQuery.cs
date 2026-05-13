using MediatR;
using Semesterprojekt1PBA.Application.Dto.School.Query;

namespace Semesterprojekt1PBA.Application.Features.School.Query;

public record GetSchoolQuery(Guid SchoolId): IRequest<GetDetailedSchoolResponse>;
 
