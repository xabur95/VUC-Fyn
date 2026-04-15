using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;

namespace Semesterprojekt1PBA.Application.Features.AssignmentSheet.Command.CreateAssignmentSheet
{
    public class CreateAssignmentSheetCommandHandler : IRequestHandler<CreateAssignmentSheetCommand, Guid>
    {
        private readonly ILogger logger;
        private readonly IUserRepository userRepository;
        private readonly IAssignmentSheetRepository _assignmentSheetrepository;
        public CreateAssignmentSheetCommandHandler()
        {
            
        }

        public Task<Guid> Handle(CreateAssignmentSheetCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
