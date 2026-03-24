using Moq;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.Policies;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Entities;

public class UserRepositoryTests
{

    [Fact]
    public void GetById_WhenUserExists_ReturnsUser()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var student = User.Create("Homer", "Simpson", "Simp@dooh.com", new RolePolicies.StudentRolePolicy(), RoleType.Student);
        mockRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(student);

        // Act
        var result = mockRepository.Object.GetById(Guid.NewGuid());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(student, result);
    }

}