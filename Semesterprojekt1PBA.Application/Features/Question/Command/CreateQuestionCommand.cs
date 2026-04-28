using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.Question.Command;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.Interfaces;

namespace Semesterprojekt1PBA.Application.Features.Question.Command;

public record CreateQuestionCommand(CreateQuestionRequest CreateQuestionRequest)
    : IRequest<Result<Guid>>, ITransactionalCommand;

public class CreateQuestionCommandHandler(
    ILogger logger,
    IQuestionRepository questionRepository,
    IUserRepository userRepository,
    ITagRepository tagRepository,
    ISubjectRepository subjectRepository)
    : IRequestHandler<CreateQuestionCommand, Result<Guid>>
{
  public async Task<Result<Guid>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
  {
    try
    {
      var req = request.CreateQuestionRequest;

      // Load creator
      var creator = await userRepository.GetByIdAsync(req.CreatedByUserId);

      // Load optional parent question
      Domain.Entities.Question? parentQuestion = null;
      if (req.ParentQuestionId.HasValue)
        parentQuestion = await questionRepository.GetQuestionByIdAsync(req.ParentQuestionId.Value);

      // Load optional tags
      IEnumerable<Domain.Entities.Tag>? tags = null;
      if (req.TagIds is { Count: > 0 })
        tags = await tagRepository.GetTagsByIdsAsync(req.TagIds);

      // Load optional subjects
      IEnumerable<Subject>? subjects = null;
      if (req.SubjectIds is { Count: > 0 })
      {
        var subjectList = new List<Subject>();
        foreach (var subjectId in req.SubjectIds)
        {
          //TODO: Implement GetByIdAsync in ISubjectRepository to load subjects by their IDs.
          // ISubjectRepository doesn't have GetByIdAsync yet;
          // when it does, replace this placeholder.
          // For now we skip subject loading — extend ISubjectRepository as needed.
        }
        subjects = subjectList.Count > 0 ? subjectList : null;
      }

      // Do
      var question = Domain.Entities.Question.Create(
          creator,
          req.Title,
          req.Text,
          req.Points,
          req.ActiveStatus,
          parentQuestion,
          tags,
          subjects);

      // Save
      await questionRepository.CreateQuestionAsync(question);

      return Result.Ok(question.Id).WithSuccess("Question created successfully.");
    }
    catch (ErrorException ex)
    {
      logger.LogError(ex, "Domain error occurred while creating the question. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}", ex.ErrorCode, ex.UserMessage);
      return Result.Fail(ex.UserMessage ?? "Failed to create Question due to a domain error.");
    }
    catch (Exception e)
    {
      logger.LogError(e, "An error occurred while creating the question.");
      return Result.Fail("Failed to create Question.");
    }
  }
}
