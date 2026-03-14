using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Domain.Test.Entities;

public class StudentTest
{

    [Fact]
    public void Create_ShouldReturnStudent()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = "poul@hansen.dk";

        // Act
        var student = Student.Create(firstName, lastName, email);

        // Assert
        Assert.Equal(firstName, student.Name.FirstName);
        Assert.Equal(lastName, student.Name.LastName);
        Assert.Equal(email, student.Email.Value);
    }
}