using Moq;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Interfaces;

namespace Semesterprojekt1PBA.Domain.Test.Entities;

public class UserRepositoryTests
{

    [Fact]
    public void GetById_WhenUserExists_ReturnsUser()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();
        var student = Student.Create("Homer", "Simpson", "Simp@dooh.com");
        mockRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns(student);

        // Act
        var result = mockRepository.Object.GetById(Guid.NewGuid());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(student, result);
    }

}