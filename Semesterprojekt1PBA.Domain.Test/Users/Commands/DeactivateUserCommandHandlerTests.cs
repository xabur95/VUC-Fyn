using MediatR;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Commands.DeactivateUser;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Users.Commands;
/// <summary>
/// Author: Michael
/// Unittests for DeactivateUserCommandHandler. Verificerer at handleren returnerer korrekt resultat
/// og interagerer med IUserRepository som forventet ved deaktivering af en bruger.
/// </summary>
public class DeactivateUserCommandHandlerTests
{
    [Fact]
    public async Task DeactivateUserCommandHandler_WhenUserIsDeactivated_ReturnsUnit()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var user = User.Create("Homer", "Simpson", "dooh@gmail.com", RoleType.Student);
        mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
        var deactivateUserCommandHandler = new DeactivateUserCommandHandler(mockRepository.Object);
        var command = new DeactivateUserCommand { Id = user.Id };

        // Act
        var result = await deactivateUserCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Unit>(result);
        mockRepository.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Once);

    }
}