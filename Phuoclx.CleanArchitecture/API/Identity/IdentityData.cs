namespace API.Identity
{
    public class IdentityData
    {
        /// <summary>
        /// Token Concern
        /// </summary>
        public const string AdminUserClaimName = "admin";

        /// <summary>
        /// Application Concern
        /// </summary>
        public const string AdminUserPolicyName = "Admin";

        public const string RoleClaimName = "role";
        public const string RolePolicyName = "Role";
        public const string ManagerUserClaimName = "manager";
        public const string ManagerUserPolicyName = "Manager";
    }
}
