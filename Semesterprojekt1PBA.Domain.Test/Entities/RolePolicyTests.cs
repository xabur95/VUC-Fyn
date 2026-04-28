using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.Policies;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Test.Entities;
/// <summary>
/// Author: Michael
/// Unit tests for role-based policy validation for Student, Teacher, and Admin roles.
/// Verifies that each role policy accepts valid roles and throws exceptions for invalid roles,
/// including scenarios where users have multiple roles.
/// </summary>
public class RolePolicyTests
{
    [Fact]
    public void StudentPolicy_WhenValidRole_DoesNotThrow()
    {
        // Arrange
        var policy = new RolePolicies.StudentRolePolicy();

        // Act
        policy.Validate(RoleType.Student, new List<UserRole>());

        // Assert
        Assert.Null(null);
    }

    [Fact]
    public void StudentPolicy_WhenInvalidRole_ThrowsErrorException()
    {
        // Arrange
        var policy = new RolePolicies.StudentRolePolicy();

        // Assert
        Assert.Throws<ErrorException>(() =>
            policy.Validate(RoleType.Teacher, new List<UserRole>()));
    }

    [Fact]
    public void AdminPolicy_WhenValidRole_DoesNotThrow()
    {
        // Arrange
        var policy = new RolePolicies.AdminRolePolicy();
        
        // Act
        policy.Validate(RoleType.Admin, new List<UserRole>());

        // Assert
        Assert.Null(null);
    }

    [Fact]
    public void AdminPolicy_WhenInvalidRole_ThrowsErrorException()
    {
        // Arrange
        var policy = new RolePolicies.AdminRolePolicy();

        // Assert
        Assert.Throws<ErrorException>(() =>
            policy.Validate(RoleType.Student, new List<UserRole>()));
    }

    [Fact]
    public void TeacherPolicy_WhenValidRole_DoesNotThrow()
    {
        // Arrange
        var policy = new RolePolicies.TeacherRolePolicy();
     
        // Act & Assert
        policy.Validate(RoleType.Teacher, new List<UserRole>());
    }

    [Fact]
    public void TeacherPolicy_WhenAdminRole_DoesNotThrow()
    {
        // Arrange
        var policy = new RolePolicies.TeacherRolePolicy();
        var userRoles = new List<UserRole>
        {
            new UserRole(RoleType.Admin)
        };
     
        // Act & Assert
        policy.Validate(RoleType.Teacher, userRoles);
    }

    [Fact]
    public void TeacherPolicy_WhenInvalidRole_ThrowsErrorException()
    {
        // Arrange
        var policy = new RolePolicies.TeacherRolePolicy();

        // Assert
        Assert.Throws<ErrorException>(() => policy.Validate(RoleType.Student, new List<UserRole>()));
    }

}
