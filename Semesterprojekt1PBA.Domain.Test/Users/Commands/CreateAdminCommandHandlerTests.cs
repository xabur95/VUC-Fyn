using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Commands.CreateAdmin;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Domain.Test.Users.Commands;
/// <summary>
/// Author: Michael
/// Contains unit tests for CreateAdminCommandHandler, verifying that an admin is created correctly
/// and that the returned Guid is valid.
/// </summary>
public class CreateAdminCommandHandlerTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly Mock<ILogger<CreateAdminCommandHandler>> _mockLogger;

    public CreateAdminCommandHandlerTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<CreateAdminCommandHandler>>();
    }

    [Fact]
    public async Task CreateAdminCommand_WhenDataIsValid_ReturnsNonEmptyGuid()
    {
        // Arrange
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Admin>())).Returns(Task.CompletedTask);
        var handler = new CreateAdminCommandHandler(_mockRepository.Object, _mockLogger.Object);
        var command = new CreateAdminCommand
        {
            FirstName = "Marge",
            LastName = "Simpson",
            Email = "marge@springfield.com"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task CreateAdminCommand_WhenDataIsValid_CallsRepositoryAddOnce()
    {
        // Arrange
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Admin>())).Returns(Task.CompletedTask);
        var handler = new CreateAdminCommandHandler(_mockRepository.Object, _mockLogger.Object);
        var command = new CreateAdminCommand
        {
            FirstName = "Marge",
            LastName = "Simpson",
            Email = "marge@springfield.com"
        };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Admin>()), Times.Once);
    }
}
