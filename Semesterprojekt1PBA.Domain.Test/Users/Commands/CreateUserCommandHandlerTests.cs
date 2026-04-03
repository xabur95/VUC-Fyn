using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Commands.CreateUser;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Users.Commands;

public class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task CreateUserCommand_WhenUserIsCreated_ReturnsGuid()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var command = new CreateUserCommand
        {
            FirstName = "Homer",
            LastName = "Simpson",
            Email = "dooh@gmail.com",
            RoleType = RoleType.Student
        };
        var createUserCommandHandler = new CreateUserCommandHandler(mockRepository.Object);

        // Act
        var result = await createUserCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Guid>(result);
        Assert.NotEqual(Guid.Empty, result);
        mockRepository.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
    }
}