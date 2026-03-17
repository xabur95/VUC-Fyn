namespace Semesterprojekt1PBA.Domain.ValueObjects
{
    public class UserRole
    {
        public RoleType RoleType { get; }

        private UserRole()
        {
        }

        public UserRole(RoleType roleType)
        {
            RoleType = roleType;
        }
    }
}

public enum RoleType
{
    Student,
    Teacher,
    Admin
}