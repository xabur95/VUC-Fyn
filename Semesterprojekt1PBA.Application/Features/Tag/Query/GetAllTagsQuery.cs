using FluentResults;
using MediatR;
using Semesterprojekt1PBA.Application.Dto.Tag.Query;

namespace Semesterprojekt1PBA.Application.Features.Tag.Query;

public record GetAllTagsQuery()
    : IRequest<Result<IEnumerable<GetTagResponse>>>;
