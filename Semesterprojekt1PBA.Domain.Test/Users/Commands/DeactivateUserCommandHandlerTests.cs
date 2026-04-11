using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Commands.DeactivateUser;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Users.Commands;
/// <summary>
/// Author: Michael
/// Unit tests for DeactivateUserCommandHandler. Verifies that the handler returns the correct result
/// and interacts with IUserRepository as expected when deactivating a user.
/// </summary>
public class DeactivateUserCommandHandlerTests
{
    [Fact]
    public async Task DeactivateUserCommandHandler_WhenUserIsDeactivated_ReturnsUnit()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockLogger = new Mock<ILogger>();
        var user = User.Create("Homer", "Simpson", "dooh@gmail.com", RoleType.Student);
        mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
        var deactivateUserCommandHandler = new DeactivateUserCommandHandler(mockRepository.Object, mockLogger.Object);
        var command = new DeactivateUserCommand { Id = user.Id };

        // Act
        var result = await deactivateUserCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Unit>(result);
        mockRepository.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Once);
    }
}