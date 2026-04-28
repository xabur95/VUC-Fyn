using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Tag.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Tag.Command;

public record CreateTagCommand(CreateTagRequest CreateTagRequest)
    : IRequest<Result<Guid>>, ITransactionalCommand;

public class CreateTagCommandHandler(
    ILogger logger,
    ITagRepository tagRepository)
    : IRequestHandler<CreateTagCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var req = request.CreateTagRequest;

            // Do
            var tag = Domain.Entities.Tag.Create(req.Title, req.Description);

            // Save
            await tagRepository.CreateTagAsync(tag);

            return Result.Ok(tag.Id).WithSuccess("Tag created successfully.");
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex, "Domain error while creating tag. ErrorCode: {ErrorCode}", ex.ErrorCode);
            return Result.Fail(ex.UserMessage ?? "Failed to create Tag due to a domain error.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while creating the tag.");
            return Result.Fail("Failed to create Tag.");
        }
    }
}
