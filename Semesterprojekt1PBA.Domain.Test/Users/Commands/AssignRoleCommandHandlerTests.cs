using Microsoft.Extensions.Logging;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Commands.AssignRole;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Users.Commands;
/// <summary>
/// Author: Michael
/// Unit tests for AssignRoleCommandHandler. Verifies that the handler correctly updates user roles
/// when handling an AssignRoleCommand. Mocked dependencies isolate the handler from IUserRepository.
/// </summary>
public class AssignRoleCommandHandlerTests
{
    [Fact]
    public async Task AssignRoleCommand_WhenRoleIsAssigned_ReturnsUnit()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockLogger = new Mock<ILogger>();
        var user = User.Create("Homer", "Simpson", "dooh@gmail.com", RoleType.Teacher);
        mockRepository.Setup(r => r.GetByIdAsync(user.Id)).ReturnsAsync(user);
        mockRepository.Setup(r => r.UpdateAsync(user));
        var assignRoleCommandHandler = new AssignRoleCommandHandler(mockRepository.Object, mockLogger.Object);
        var query = new AssignRoleCommand { Id = user.Id, RoleType = RoleType.Admin };

        // Act
        var result = await assignRoleCommandHandler.Handle(query, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Once);
    }
}