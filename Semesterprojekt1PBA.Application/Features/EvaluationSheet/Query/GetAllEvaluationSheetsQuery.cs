using MediatR;
using Semesterprojekt1PBA.Application.Dto.EvaluationSheet.Query;

namespace Semesterprojekt1PBA.Application.Features.EvaluationSheet.Query;

public record GetAllEvaluationSheetsQuery()
    : IRequest<IEnumerable<GetEvaluationSheetResponse>>;
