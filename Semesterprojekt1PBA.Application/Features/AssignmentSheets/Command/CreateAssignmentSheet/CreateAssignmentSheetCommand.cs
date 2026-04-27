using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Application.Features.AssignmentSheets.Command.CreateAssignmentSheet
{
    public record CreateAssignmentSheetCommand : IRequest<Guid>, ITransactionalCommand
    {
        public required AssignmentSheet NewAsignmentSheet { get; set; }
    }
}
