using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Commands.CreateStudent;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Domain.Test.Users.Commands;
/// <summary>
/// Author: Michael
/// Contains unit tests for CreateStudentCommandHandler, verifying that a student is created correctly
/// and that the returned Guid is valid.
/// </summary>
public class CreateStudentCommandHandlerTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly Mock<ILogger<CreateStudentCommandHandler>> _mockLogger;

    public CreateStudentCommandHandlerTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<CreateStudentCommandHandler>>();
    }

    [Fact]
    public async Task CreateStudentCommand_WhenDataIsValid_ReturnsNonEmptyGuid()
    {
        // Arrange
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Student>())).Returns(Task.CompletedTask);
        var handler = new CreateStudentCommandHandler(_mockRepository.Object, _mockLogger.Object);
        var command = new CreateStudentCommand
        {
            FirstName = "Bart",
            LastName = "Simpson",
            Email = "bart@springfield.com",
            Knr = "12345",
            Tilmeldt = DateOnly.FromDateTime(DateTime.Now),
            Ophørt = null
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task CreateStudentCommand_WhenDataIsValid_CallsRepositoryAddOnce()
    {
        // Arrange
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Student>())).Returns(Task.CompletedTask);
        var handler = new CreateStudentCommandHandler(_mockRepository.Object, _mockLogger.Object);
        var command = new CreateStudentCommand
        {
            FirstName = "Bart",
            LastName = "Simpson",
            Email = "bart@springfield.com",
            Knr = "12345",
            Tilmeldt = DateOnly.FromDateTime(DateTime.Now),
            Ophørt = null
        };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Student>()), Times.Once);
    }
}
