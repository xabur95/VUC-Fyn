using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Queries.GetUserById;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Users.Queries;

/// <summary>
/// Author: Michael
/// Test for GetUserByIdQueryHandler. Verificerer at handleren returnerer korrekt svar når en bruger findes,
/// og kaster InvalidOperationException når brugeren ikke eksisterer.
/// </summary>
public class GetUserByIdQueryHandlerTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly User _user;

    public GetUserByIdQueryHandlerTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _user = User.Create("Homer", "Simpson", "dooh@gmail.com", RoleType.Student);
    }

    [Fact]
    public async Task GetUserByIdQuery_WhenUserExists_ReturnsGetUserByIdResponse()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(_user.Id)).ReturnsAsync(_user);
        var getUserByIdQueryHandler = new GetUserByIdQueryHandler(_mockRepository.Object);
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
    public async Task GetUserByIdQuery_WhenUserDoNotExists_ThrowsInvalidOperationException()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))!.ReturnsAsync((User?)null);
        var getUserByIdQueryHandler = new GetUserByIdQueryHandler(_mockRepository.Object);
        var query = new GetUserByIdQuery { Id = Guid.NewGuid() };

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            getUserByIdQueryHandler.Handle(query, CancellationToken.None));
    }
}