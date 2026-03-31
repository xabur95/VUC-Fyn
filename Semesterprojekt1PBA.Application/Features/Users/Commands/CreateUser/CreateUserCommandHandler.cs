using MediatR;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.CreateUser;
/// <summary>
/// Author: Michael
/// Håndterer oprettelse af en ny bruger via en Command.
/// Behandler en CreateUserCommand og gemmer brugeren via et repository.
/// </summary>

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = User.Create(request.FirstName, request.LastName, request.Email, request.RoleType);

        await _userRepository.AddAsync(user);

        return user.Id;
    }
}