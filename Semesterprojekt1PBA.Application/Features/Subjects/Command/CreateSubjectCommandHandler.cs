using MediatR;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Subjects.Command
{
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Guid>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger _logger;

        public CreateSubjectCommandHandler(ISubjectRepository subjectRepository, ILogger logger)
        {
            _subjectRepository = subjectRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Subject subject = Subject.Create(request.Name, request.Level);

                await _subjectRepository.AddAsync(subject);

                return subject.Id;
            }
            catch (ErrorException ex)
            {
                _logger.LogError(ex, "Domain error occurred while creating subject. ErrorCode: {ErrorCode}, UserMessage: {UserMessage}", ex.ErrorCode, ex.UserMessage);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating subject.");
                throw;
            }
        }
    }
}
