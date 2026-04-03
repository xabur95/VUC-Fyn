using Moq;
using Semesterprojekt1PBA.Application.Features.Users.Queries;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Users.Queries;
/// <summary>
/// Author: Michael
/// Enhedstests for GetUserByIdQueryHandler. Verificerer at handleren returnerer korrekt svar når en bruger findes,
/// og kaster InvalidOperationException når brugeren ikke eksisterer. Mockede afhængigheder isolerer handleren fra eksterne lag.
/// </summary>
public class GetUserByIdQueryHandlerTests
{
    [Fact]
    public async Task GetUserByIdQuery_WhenUserExists_ReturnsGetUserByIdResponse()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var user = User.Create("Homer", "Simpson", "dooh@gmail.com", RoleType.Student);
        mockRepository.Setup(r => r.GetByIdAsync(user.Id)).ReturnsAsync(user);
        var getUserByIdQueryHandler = new GetUserByIdQueryHandler(mockRepository.Object);
        var query = new GetUserByIdQuery { Id = user.Id };

        // Act
        var result = await getUserByIdQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal("Homer",result.FirstName);
        Assert.Equal("Simpson",result.LastName);
        Assert.Equal("dooh@gmail.com",result.Email);
        Assert.Contains(result.Roles, r => r == RoleType.Student);
    }

    [Fact]
    public async Task GetUserByIdQuery_WhenUserDoNotExists_ThrowsInvalidOperationException()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))!.ReturnsAsync((User?)null);
        var getUserByIdQueryHandler = new GetUserByIdQueryHandler(mockRepository.Object);
        var query = new GetUserByIdQuery { Id = Guid.NewGuid() };
        
        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => getUserByIdQueryHandler.Handle(query, CancellationToken.None));
    }
}