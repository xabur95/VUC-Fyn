using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Tag.Query;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Tag.Query;

public class GetTagByIdQueryHandler(
    ILogger logger,
    ITagRepository tagRepository)
    : IRequestHandler<GetTagByIdQuery, Result<GetTagResponse>>
{
    public async Task<Result<GetTagResponse>> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var tag = await tagRepository.GetTagByIdAsync(request.TagId);

            var response = new GetTagResponse(
                tag.Id,
                tag.RowVersion,
                tag.Title.Value,
                tag.Description);

            return Result.Ok(response);
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex, "Domain error fetching tag {TagId}. ErrorCode: {ErrorCode}", request.TagId, ex.ErrorCode);
            return Result.Fail(ex.UserMessage ?? "Failed to retrieve the tag.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching tag {TagId}", request.TagId);
            return Result.Fail("Something went wrong while fetching the tag.");
        }
    }
}
