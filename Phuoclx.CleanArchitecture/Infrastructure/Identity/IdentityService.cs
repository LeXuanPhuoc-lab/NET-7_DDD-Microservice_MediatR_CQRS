using Application.Common.Interfaces;
namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IRoleManagerService _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager,
            IRoleManagerService roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<string> CreateAsync(ApplicationUser user, string password, string role)
        {
            var identityRole = await _roleManager.GetByNameAsync(role);
            if (identityRole == null) return null!;

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, identityRole.Name!);
                var createdUser = await _userManager.FindByEmailAsync(user.Email!);
                return createdUser!.Id;
            }
            return null!;
        }
        public Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            throw new NotImplementedException();
        }
        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null ? user : null!;
        }
        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return user!.Id;
        }
        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return user != null && await _userManager.IsInRoleAsync(user, role);
        }
        public async Task<IEnumerable<string>> GetRolesByUserIdAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var roles = await _userManager.GetRolesAsync(user!);
            return roles != null ? roles.ToArray() : null!;
        }
    }
}
