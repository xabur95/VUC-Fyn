using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Commands.CreateTeacher;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Domain.Test.Users.Commands;
/// <summary>
/// Author: Michael
/// Contains unit tests for CreateTeacherCommandHandler, verifying that a teacher is created correctly
/// and that the returned Guid is valid.
/// </summary>
public class CreateTeacherCommandHandlerTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly Mock<ILogger<CreateTeacherCommandHandler>> _mockLogger;

    public CreateTeacherCommandHandlerTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<CreateTeacherCommandHandler>>();
    }

    [Fact]
    public async Task CreateTeacherCommand_WhenDataIsValid_ReturnsNonEmptyGuid()
    {
        // Arrange
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Teacher>())).Returns(Task.CompletedTask);
        var handler = new CreateTeacherCommandHandler(_mockRepository.Object, _mockLogger.Object);
        var command = new CreateTeacherCommand
        {
            FirstName = "Ned",
            LastName = "Flanders",
            Email = "ned@springfield.com"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task CreateTeacherCommand_WhenDataIsValid_CallsRepositoryAddOnce()
    {
        // Arrange
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Teacher>())).Returns(Task.CompletedTask);
        var handler = new CreateTeacherCommandHandler(_mockRepository.Object, _mockLogger.Object);
        var command = new CreateTeacherCommand
        {
            FirstName = "Ned",
            LastName = "Flanders",
            Email = "ned@springfield.com"
        };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Teacher>()), Times.Once);
    }
}
