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