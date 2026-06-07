using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Dto.AssignmentSheet.Query;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.AssignmentSheets.Query.GetAssignmentSheetById;

public class GetAssignmentSheetByIdQueryHandler(
    ILogger<GetAssignmentSheetByIdQueryHandler> logger,
    IAssignmentSheetRepository assignmentSheetRepository)
    : IRequestHandler<GetAssignmentSheetByIdQuery, GetAssignmentSheetResponse>
{
    public async Task<GetAssignmentSheetResponse> Handle(GetAssignmentSheetByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var sheet = await assignmentSheetRepository.GetByIdAsync(request.AssignmentSheetId);

            return new GetAssignmentSheetResponse(
                sheet.Id,
                sheet.RowVersion,
                sheet.Author.Id,
                sheet.Subject.Id,
                sheet.Subject.Title.Value,
                sheet.Topics.Select(t => new GetAssignmentSheetTopicResponse(t.Id, t.Name)),
                sheet.Questions.Select(q => new GetAssignmentSheetQuestionResponse(
                    q.Id,
                    q.Title.Value,
                    q.Text,
                    q.Points)));
        }
        catch (ErrorException ex)
        {
            logger.LogError(ex, "Domain error occurred while fetching assignment sheet {Id}. ErrorCode: {ErrorCode}",
                request.AssignmentSheetId, ex.ErrorCode);
            throw;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error fetching assignment sheet with id {Id}", request.AssignmentSheetId);
            throw;
        }
    }
}
