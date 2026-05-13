using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Tag.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Tag.Command;

public record UpdateTagCommand(UpdateTagRequest UpdateTagRequest)
    : IRequest<bool>, ITransactionalCommand;

public class UpdateTagCommandHandler(
    ILogger logger,
    ITagRepository tagRepository)
    : IRequestHandler<UpdateTagCommand, bool>
{
  public async Task<bool> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var req = request.UpdateTagRequest;

      // Load
      var tag = await tagRepository.GetTagByIdAsync(req.TagId);

      // Do
      tag.Rename(req.Title);
      tag.UpdateDescription(req.Description);

      // Save
      await tagRepository.UpdateTagAsync(tag);

      return true;
    }
    catch (ErrorException ex)
    {
      logger.LogError(ex, "Domain error while updating tag {TagId}. ErrorCode: {ErrorCode}",
          request.UpdateTagRequest.TagId, ex.ErrorCode);
      throw;
    }
    catch (Exception e)
    {
      logger.LogError(e, "Error updating tag {TagId}", request.UpdateTagRequest.TagId);
      throw;
    }
  }
}
