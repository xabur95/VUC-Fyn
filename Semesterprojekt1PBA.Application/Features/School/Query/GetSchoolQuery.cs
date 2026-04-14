using FluentResults;
using MediatR;
using Semesterprojekt1PBA.Application.Dto.School.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Application.Features.School.Query;

public record GetSchoolQuery(Guid SchoolId): IRequest<Result<GetDetailedSchoolResponse>>;
 
