using MediatR;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Application.Features.Users.Commands.UpdateUser;
/// <summary>
/// Author: Michael
/// Håndterer opdatering af en bruger ved at behandle en UpdateUserCommand.
/// Henter brugeren via ID, anvender opdateringerne og gemmer ændringerne i repository'et.
/// </summary>
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
 {
     private readonly IUserRepository _userRepository;

     public UpdateUserCommandHandler(IUserRepository userRepository)
     {
         _userRepository = userRepository;
     }
    
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetByIdAsync(request.Id);

        user.Update(request.FirstName, request.LastName, request.Email);

        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}