using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Tag.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Tag.Command;

public record CreateTagCommand(CreateTagRequest CreateTagRequest)
    : IRequest<Guid>, ITransactionalCommand;

public class CreateTagCommandHandler(
    ILogger<CreateTagCommandHandler> logger,
    ITagRepository tagRepository)
    : IRequestHandler<CreateTagCommand, Guid>
{
  public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var req = request.CreateTagRequest;

      // Do
      var tag = Domain.Entities.Tag.Create(req.Title, req.Description);

      // Save
      await tagRepository.CreateTagAsync(tag);

      return tag.Id;
    }
    catch (ErrorException ex)
    {
      logger.LogError(ex, "Domain error while creating tag. ErrorCode: {ErrorCode}", ex.ErrorCode);
      throw;
    }
    catch (Exception e)
    {
      logger.LogError(e, "An error occurred while creating the tag.");
      throw;
    }
  }
}
