using MediatR;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.CreateStudent;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    public CreateStudentCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var user 
            = Student.Create(request.FirstName, request.LastName, request.Email, request.Knr,
            request.Tilmeldt, request.Ophørt);

        await _userRepository.AddAsync(user);

        return user.Id;
    }
}
