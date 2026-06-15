using MediatR;
using Semesterprojekt1PBA.Application.Dto.Class.Query;

namespace Semesterprojekt1PBA.Application.Features.Class.Query;

public record GetAllClassesQuery : IRequest<List<GetAllClassesResponse>>;
