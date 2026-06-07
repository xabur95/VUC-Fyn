using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Tag.Query;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;

namespace Semesterprojekt1PBA.Application.Features.Tag.Query;

public class GetAllTagsQueryHandler(
    ILogger<GetAllTagsQueryHandler> logger,
    ITagRepository tagRepository)
    : IRequestHandler<GetAllTagsQuery, IEnumerable<GetTagResponse>>
{
  public async Task<IEnumerable<GetTagResponse>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
  {
    try
    {
      var tags = await tagRepository.GetAllTagsAsync();

      var responses = tags.Select(t => new GetTagResponse(
          t.Id,
          t.RowVersion,
          t.Title.Value,
          t.Description));

      return responses;
    }
    catch (Exception e)
    {
      logger.LogError(e, "Error fetching all tags.");
      throw;
    }
  }
}
