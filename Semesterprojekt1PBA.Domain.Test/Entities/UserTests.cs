using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Entities;

public class UserTests
{

    public static IEnumerable<object[]> ValidTeacher =>
        new List<object[]>
        {
            new object[] { "Homer", "Simpson", "dooh@yahoo.com", true }
        };


    [Theory]
    [MemberData(nameof(ValidTeacher))]
    public void CreateTeacher_ShouldReturnTeacherRoleAndAdminRole_WhenIsAlsoAdminIsTrue(string firstName, string lastName, string email, bool isAlsoAdmin )
    {
        // Act
        var user = User.CreateTeacher(firstName, lastName, email, isAlsoAdmin);

        // Assert
        Assert.Contains(user.Roles, r => r.RoleType == RoleType.Teacher);
        Assert.Contains(user.Roles, e => e.RoleType == RoleType.SchoolAdmin);

    }
} 