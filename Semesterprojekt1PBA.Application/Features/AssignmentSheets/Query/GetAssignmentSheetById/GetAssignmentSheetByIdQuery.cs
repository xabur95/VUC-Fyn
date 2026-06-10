using MediatR;
using Semesterprojekt1PBA.Application.Dto.AssignmentSheet.Query;

namespace Semesterprojekt1PBA.Application.Features.AssignmentSheets.Query.GetAssignmentSheetById;

public record GetAssignmentSheetByIdQuery(Guid AssignmentSheetId)
    : IRequest<GetAssignmentSheetResponse>;
