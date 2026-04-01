using MediatR;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Application.Features.Subjects.Command
{
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, Guid>
    {
        private readonly ISubjectRepository _subjectRepository;

        public CreateSubjectCommandHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Guid> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            Subject subject = Subject.Create(request.Name, request.Level);

            await _subjectRepository.AddAsync(subject);

            return subject.Id;
        }
    }
}
