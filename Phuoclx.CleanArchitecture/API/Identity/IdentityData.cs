using System.Security.Claims;

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

        public const string RoleClaimName = ClaimTypes.Role;
        public const string RolePolicyName = "Role";
        public const string ManagerUserClaimName = "manager";
        public const string ManagerUserPolicyName = "Manager";
    }
}
