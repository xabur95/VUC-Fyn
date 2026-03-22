using Semesterprojekt1PBA.Domain.Entities;

namespace Semesterprojekt1PBA.Domain.Test.Entities;
/// <summary>
/// Author: Michael
/// Unit tests for at verificere Student class adfærd.
/// </summary>
/// <remarks>
/// Bruger xUnit framework til at sikre Student class opretter instanser med forventede property værdier.
/// </remarks>
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