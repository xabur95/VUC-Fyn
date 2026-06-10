using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Tag.Query;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Tag.Query;

public class GetTagByIdQueryHandler(
    ILogger<GetTagByIdQueryHandler> logger,
    ITagRepository tagRepository)
    : IRequestHandler<GetTagByIdQuery, GetTagResponse>
{
  public async Task<GetTagResponse> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
  {
    try
    {
      var tag = await tagRepository.GetTagByIdAsync(request.TagId);

      var response = new GetTagResponse(
          tag.Id,
          tag.RowVersion,
          tag.Title.Value,
          tag.Description);

      return response;
    }
    catch (ErrorException ex)
    {
      logger.LogError(ex, "Domain error fetching tag {TagId}. ErrorCode: {ErrorCode}", request.TagId, ex.ErrorCode);
      throw;
    }
    catch (Exception e)
    {
      logger.LogError(e, "Error fetching tag {TagId}", request.TagId);
      throw;
    }
  }
}
