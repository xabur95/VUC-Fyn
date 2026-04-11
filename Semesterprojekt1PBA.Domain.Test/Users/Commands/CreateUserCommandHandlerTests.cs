using Microsoft.Extensions.Logging;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Commands.CreateUser;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Users.Commands;
/// <summary>
/// Author: Michael
/// Unit tests for CreateUserCommandHandler. Verifies that the handler correctly creates a new user
/// and returns the expected result. Mocked dependencies isolate the handler from IUserRepository.
/// </summary>
public class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task CreateUserCommand_WhenUserIsCreated_ReturnsGuid()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockLogger = new Mock<ILogger>();

        var command = new CreateUserCommand
        {
            FirstName = "Homer",
            LastName = "Simpson",
            Email = "dooh@gmail.com",
            RoleType = RoleType.Student
        };
        var createUserCommandHandler = new CreateUserCommandHandler(mockRepository.Object, mockLogger.Object);

        // Act
        var result = await createUserCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Guid>(result);
        Assert.NotEqual(Guid.Empty, result);
        mockRepository.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
    }
}