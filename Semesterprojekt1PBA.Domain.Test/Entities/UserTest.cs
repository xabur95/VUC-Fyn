using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Test.Entities;
/// <summary>
/// Author: Michael
/// Unit tests for verifying User class behavior.
/// </summary>
public class UserTest
{
    public static IEnumerable<object[]> ValidUserData =>
        new List<object[]>
        {
            new object[] { "Homer", "Simpson", "dooh@yahoo.com" },
            new object[] { "Marge", "Simpson", "mommy@gmail.com" }
        };

    public static IEnumerable<object[]> InvalidUserData =>
        new List<object[]>
        {
            new object[] { "Bart4", "Simpson", "@wrong_1email.i" },
            new object[] { "L1sa", "Simps0n", "wrong_2email.com" }
        };

  
    [Theory]
    [MemberData(nameof(InvalidUserData))]
    public void Create_WhenInvalidData_ThrowsErrorException(string firstName, string lastName, string email)
    {
        // Act
        var user = () => Admin.Create(firstName, lastName, email);

        // Assert
        Assert.Throws<ErrorException>(user);
    }

    [Fact]
    public void AssignRole_WhenRoleIsValid_ShouldAssignRole()
    {
        // Arrange
        var user = Teacher.Create("Carl", "Carlson", "carl@gmail.com");

        // Act
        user.AssignRole(new UserRole(RoleType.Admin));

        // Assert
        Assert.Contains(user.Roles, r => r.RoleType == RoleType.Admin);
    }

    [Fact]
    public void AssignRole_WhenRoleIsAlreadyAssigned_ThrowsErrorException()
    {
        // Arrange
        var user = Student.Create("Apu", "Nahasapeemapetilon", "apu@indiangmail.com", "12345", DateOnly.FromDateTime(DateTime.Now), null);

        // Assert
        Assert.Throws<ErrorException>(() => user.AssignRole(new UserRole(RoleType.Student)));
    }

    [Fact]
    public void RevokeRole_WhenRoleExists_RemovesRole()
    {                                                                                        // Arrange
        var user = Teacher.Create("Lenny", "Leonard", "leeeennnyyy@wallmart.com");
        user.AssignRole(new UserRole(RoleType.Admin));

        // Act
        user.RevokeRole(new UserRole(RoleType.Teacher));

        // Assert
        Assert.DoesNotContain(user.Roles, r => r.RoleType == RoleType.Teacher);
    }

    [Fact]
    public void RevokeRole_WhenRoleDoesNotExist_ThrowsException()
    {
        // Arrange
        var user = Student.Create("Maggie", "Simpson", "thababy@tahoo.com", "12345", DateOnly.FromDateTime(DateTime.Now), null);
        
        // Assert
        Assert.Throws<ErrorException>(() => user.RevokeRole(new UserRole(RoleType.Admin)));
    }

    [Fact]
    public void Update_ShouldChangeNameAndEmail()
    {
        // Arrange
        var user = Admin.Create("Abe", "Simpson", "grandpa@yahoo.com");

        // Act
        user.Update("Abraham", "Simpson", "hotDaddy@gmail.com");

        // Assert
        Assert.Equal("Abraham", user.Name.FirstName);
        Assert.Equal("Simpson", user.Name.LastName);
        Assert.Equal("hotDaddy@gmail.com", user.Email.Value);
    }
}