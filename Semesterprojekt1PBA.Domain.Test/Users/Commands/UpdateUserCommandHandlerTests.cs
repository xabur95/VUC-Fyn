using MediatR;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Commands.UpdateUser;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Users.Commands;
/// <summary>
/// Author: Michael
/// Unittest for UpdateUserCommandHandler. Verificerer at handleren opdaterer brugerdata korrekt
/// og interagerer med IUserRepository som forventet.
/// </summary>
public class UpdateUserCommandHandlerTests
{
    [Fact]
    public async Task UpdateUserCommandHandler_WhenUserIsUpdated_ReturnsUnit()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var user = User.Create("Homer", "Simpson", "dooh@gmail.com", RoleType.Student);
        mockRepository.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync(user);
        var updateUserCommandHandler = new UpdateUserCommandHandler(mockRepository.Object);
        var command = new UpdateUserCommand
        {
            Id = user.Id,
            FirstName = "Homer",
            LastName = "Simpson",
            Email = "newEmail@gmail.com"
        };

        // Act
        var result = await updateUserCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Unit>(result);
        mockRepository.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Once);


    }
}