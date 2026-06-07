using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;

namespace Semesterprojekt1PBA.Application.Features.AssignmentSheets.Command.CreateAssignmentSheet
{
    public class CreateAssignmentSheetCommandHandler : IRequestHandler<CreateAssignmentSheetCommand, Guid>
    {
        private readonly ILogger<CreateAssignmentSheetCommandHandler> _logger;
        private readonly IAssignmentSheetRepository _assignmentSheetrepository;

        public CreateAssignmentSheetCommandHandler(ILogger<CreateAssignmentSheetCommandHandler> logger, IAssignmentSheetRepository assignmentSheetRepository)
        {
            _logger = logger;
            _assignmentSheetrepository = assignmentSheetRepository;
        }

        public async Task<Guid> Handle(CreateAssignmentSheetCommand request, CancellationToken cancellationToken)
        {
            await _assignmentSheetrepository.AddAsync(request.NewAsignmentSheet);
            return request.NewAsignmentSheet.Id;            
        }
    }
}
