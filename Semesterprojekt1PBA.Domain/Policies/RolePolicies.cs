using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Policies;

public static class RolePolicies
{
    public class StudentRolePolicy : IRolePolicy
    {
        public void Validate(RoleType newRole, IEnumerable<UserRole> currentRoles)
        {
            if (newRole != RoleType.Student)
            {
                throw new InvalidOperationException("Invalid role type for Student.");
            }
        }
    }

    public class TeacherRolePolicy : IRolePolicy
    {
        public void Validate(RoleType newRole, IEnumerable<UserRole> currentRoles)
        {
            if (newRole is not RoleType.Teacher and not RoleType.Admin)
            {
                throw new InvalidOperationException("Invalid role type for Teacher. Expected Teacher or Admin.");
            }

            if (newRole == RoleType.Admin && !currentRoles.Any(r => r.RoleType == RoleType.Teacher))
            {
                throw new InvalidOperationException("Admin role can only be assigned to a user who already has the Teacher role.");
            }
        }
    }

    public class AdminRolePolicy : IRolePolicy
    {
        public void Validate(RoleType newRole, IEnumerable<UserRole> currentRoles)
        {
            if (newRole != RoleType.Admin)
                throw new InvalidOperationException("Invalid role type for Admin.");
        }
    }
}