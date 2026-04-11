using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Policies;
/// <summary>
/// Author: Michael
/// Contains role policies for Student, Teacher, and Admin.
/// Each nested class implements IRolePolicy and enforces validation rules
/// to ensure valid role assignments.
/// </summary>
public static class RolePolicies
{
    public class StudentRolePolicy : IRolePolicy
    {
        public void Validate(RoleType newRole, IEnumerable<UserRole> currentRoles)
        {
            if (newRole != RoleType.Student)
            {
                throw new ErrorException($"Invalid role type for Student", errorCode: "INVALID_ROLE");
            }
        }
    }

    public class TeacherRolePolicy : IRolePolicy
    {
        public void Validate(RoleType newRole, IEnumerable<UserRole> currentRoles)
        {
            if (newRole is not RoleType.Teacher and not RoleType.Admin)
            {
                throw new ErrorException($"Invalid role type for Teacher. Expected Teacher or Admin.", errorCode: "INVALID_ROLE");
            }

            if (newRole == RoleType.Admin && !currentRoles.Any(r => r.RoleType == RoleType.Teacher))
            {
                throw new ErrorException($"Admin role can only be assigned to a user who already has the Teacher role.", errorCode: "INVALID_ROLE");
            }
        }
    }
    
    public class AdminRolePolicy : IRolePolicy
    {
        public void Validate(RoleType newRole, IEnumerable<UserRole> currentRoles)
        {
            if (newRole != RoleType.Admin)
            {
                throw new ErrorException($"Invalid role type for Admin.", errorCode: "INVALID_ROLE");
            }
        }
    }
}