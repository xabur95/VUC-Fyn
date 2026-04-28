using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Commands.UpdateUser;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Test.Users.Commands;
/// <summary>
/// Author: Michael
/// Unit tests for UpdateUserCommandHandler. Verifies that the handler correctly updates user data
/// and interacts with IUserRepository as expected.
/// </summary>
public class UpdateUserCommandHandlerTests
{
    [Fact]
    public async Task UpdateUserCommandHandler_WhenUserIsUpdated_ReturnsUnit()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockLogger = new Mock<ILogger>();
        var user = User.Create("Homer", "Simpson", "dooh@gmail.com", RoleType.Student);
        mockRepository.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync(user);
        var updateUserCommandHandler = new UpdateUserCommandHandler(mockRepository.Object, mockLogger.Object);
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