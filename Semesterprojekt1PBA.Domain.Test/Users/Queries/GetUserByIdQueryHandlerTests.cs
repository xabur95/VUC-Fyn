using Microsoft.Extensions.Logging;
using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Queries.GetUserById;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Test.Users.Queries;
/// <summary>
/// Author: Michael
/// Unit tests for GetUserByIdQueryHandler. Verifies that the handler returns the correct response
/// when a user is found, and throws an InvalidOperationException when the user does not exist.
/// </summary>
public class GetUserByIdQueryHandlerTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly User _user;
    private readonly Mock<ILogger> _mockLogger;

    public GetUserByIdQueryHandlerTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _user = Student.Create("Homer", "Simpson", "dooh@gmail.com", "12345", DateOnly.FromDateTime(DateTime.Now), null);
        _mockLogger = new Mock<ILogger>();
    }

    [Fact]
    public async Task GetUserByIdQuery_WhenUserExists_ReturnsGetUserByIdResponse()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(_user.Id)).ReturnsAsync(_user);
        var getUserByIdQueryHandler = new GetUserByIdQueryHandler(_mockRepository.Object, _mockLogger.Object);
        var query = new GetUserByIdQuery { Id = _user.Id };

        // Act
        var result = await getUserByIdQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal("Homer", result.FirstName);
        Assert.Equal("Simpson", result.LastName);
        Assert.Equal("dooh@gmail.com", result.Email);
        Assert.Contains(result.Roles, r => r == RoleType.Student);
    }

    [Fact]
    public async Task GetUserByIdQuery_WhenUserDoNotExists_ThrowsErrorException()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))!.ReturnsAsync((User?)null);
        var getUserByIdQueryHandler = new GetUserByIdQueryHandler(_mockRepository.Object, _mockLogger.Object);
        var query = new GetUserByIdQuery { Id = Guid.NewGuid() };

        // Assert
        await Assert.ThrowsAsync<ErrorException>(() => getUserByIdQueryHandler.Handle(query, CancellationToken.None));
    }
}