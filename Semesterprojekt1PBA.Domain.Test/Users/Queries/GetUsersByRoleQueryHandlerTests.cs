using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Queries.GetUsersByRole;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Users.Queries;
/// <summary>
/// Author: Michael
/// Contains unit tests for the GetUsersByRoleQueryHandler class, verifying that users are correctly retrieved by role.
/// These tests use mock implementations of IUserRepository and ILogger to isolate the behavior of
/// GetUsersByRoleQueryHandler. The tests ensure that the handler returns all users with the specified role when such
/// users exist. </summary>
public class GetUsersByRoleQueryHandlerTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly Mock<ILogger<GetUsersByRoleQueryHandler>> _mockLogger;

    public GetUsersByRoleQueryHandlerTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<GetUsersByRoleQueryHandler>>();
    }

    [Fact]
    public async Task GetUsersByRoleQuery_WhenUsersExist_ReturnsAllUsersWithRole()
    {
        // Arrange
        var student1 = Student.Create("Homer", "Simpson", "homer@gmail.com", "12345", DateOnly.FromDateTime(DateTime.Now), null);
        var student2 = Student.Create("Bart", "Simpson", "bart@gmail.com", "67890", DateOnly.FromDateTime(DateTime.Now), null);
        var users = new List<User> { student1, student2 };

        _mockRepository.Setup(r => r.GetByRoleAsync(RoleType.Student)).ReturnsAsync(users);
        var handler = new GetUsersByRoleQueryHandler(_mockRepository.Object, _mockLogger.Object);
        var query = new GetUsersByRoleQuery { RoleType = RoleType.Student };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(u => u.FirstName == "Homer");
        result.Should().Contain(u => u.FirstName == "Bart");
    }

    [Fact]
    public async Task GetUsersByRoleQuery_WhenUserDoesNotExists_ReturnsEmptyList()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByRoleAsync(RoleType.Admin)).ReturnsAsync(new List<User>());
        
        var handler = new GetUsersByRoleQueryHandler(_mockRepository.Object, _mockLogger.Object);
        var query = new GetUsersByRoleQuery { RoleType = RoleType.Admin };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEmpty();
    }
}

