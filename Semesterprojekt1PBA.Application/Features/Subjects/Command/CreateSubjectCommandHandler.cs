using MediatR;
using Microsoft.Extensions.Logging;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Application.Features.Subjects.Command
{
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Guid>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger<CreateSubjectCommandHandler> _logger;

        public CreateSubjectCommandHandler(ISubjectRepository subjectRepository, ILogger<CreateSubjectCommandHandler> logger)
        {
            _subjectRepository = subjectRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var otherSubjects = await _subjectRepository.GetAllSubjectsAsync();
                Subject subject = Subject.Create(request.Title, request.Level, otherSubjects);

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
