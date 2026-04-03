using MediatR;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Commands.AssignRole;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Users.Commands;

public class AssignRoleCommandHandlerTests
{

    [Fact]
    public async Task AssignRoleCommand_WhenRoleIsAssigned_ReturnsUnit()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var user = User.Create("Homer", "Simpson", "dooh@gmail.com", RoleType.Teacher);
        mockRepository.Setup(r => r.GetByIdAsync(user.Id)).ReturnsAsync(user);
        mockRepository.Setup(r => r.UpdateAsync(user));
        var assignRoleCommandHandler = new AssignRoleCommandHandler(mockRepository.Object);
        var query = new AssignRoleCommand { Id = user.Id, RoleType = RoleType.Admin };

        // Act
        var result = await assignRoleCommandHandler.Handle(query, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Once);
    }
}