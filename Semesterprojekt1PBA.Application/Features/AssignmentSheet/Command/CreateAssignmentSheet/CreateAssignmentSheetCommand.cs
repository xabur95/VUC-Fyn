using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Application.Features.AssignmentSheet.Command.CreateAssignmentSheet
{
    public record CreateAssignmentSheetCommand : IRequest<Guid>, ITransactionalCommand
    {
        public Guid Author { get; set; }
        public Guid Subject { get; set; }

    }
}
