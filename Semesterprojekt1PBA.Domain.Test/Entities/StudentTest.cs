using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Policies;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Entities;
/// <summary>
/// Author: Michael
/// Unit tests for at verificere Student class adfærd.
/// </summary>

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
        var student = User.Create(firstName, lastName, email, new RolePolicies.StudentRolePolicy(), RoleType.Student);

        // Assert
        Assert.Equal(firstName, student.Name.FirstName);
        Assert.Equal(lastName, student.Name.LastName);
        Assert.Equal(email, student.Email.Value);
    }
}