using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Commands.RevokeRole;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Test.Users.Commands;
/// <summary>
/// Author: Michael
/// Unit tests for RevokeRoleCommandHandler. Verifies that the handler correctly updates user roles
/// and interacts with IUserRepository as expected. Mocked dependencies isolate the handler from external layers.
/// </summary>
public class RevokeRoleCommandHandlerTests
{
    [Fact]
    public async Task RevokeRoleCommandHandler_WhenRoleIsRevoked_ReturnsUnit()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var mockLogger = new Mock<ILogger>();
        var user = User.Create("Homer", "Simpson", "dooh@gmail.com", RoleType.Teacher);
        user.AssignRole(new UserRole(RoleType.Admin));
        mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
        var revokeCommandHandler = new RevokeRoleCommandHandler(mockRepository.Object, mockLogger.Object);
        var command = new RevokeRoleCommand
        {
            Id = user.Id,
            RoleType = RoleType.Admin
        };

        // Act
        var result = await revokeCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Unit>(result);
        mockRepository.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Once);
    }
}