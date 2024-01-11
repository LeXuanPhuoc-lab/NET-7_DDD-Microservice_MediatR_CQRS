using Infrastructure.Data;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Threading;

namespace Infrastructure.Repository
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly APIContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityRepository(APIContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result<ApplicationUser>> CreateAsync(ApplicationUser user, string password, string role)
        {
            try
            {
                // get user by email
                var identityUser = await GetByEmailAsync(user.Email!);
                // email already exist
                if (identityUser is not null)
                    throw new DuplicateNameException("Email already exist");

                // create new user
                var result = await _userManager.CreateAsync(user, password);

                // add role for user
                if (result.Succeeded)
                {
                    var identityRole = await _roleManager.FindByNameAsync(role);
                    if (identityRole is not null) await _userManager.AddToRoleAsync(user, identityRole.Name!);
                    return user;
                };
            }
            catch(Exception ex) 
            {
                // _logger.LogError(//Message);
                // throw;
            }
            return null!;
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            try
            {
                return await _userManager.FindByNameAsync(email);
            }
            catch(Exception ex)
            {
                // _logger.LogError(//Message);
                // throw;
            }
            return null!;
        }

        public async Task<ApplicationUser?> GetByIdAsync(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<IEnumerable<string>> GetRolesbyUserIdAsync(string userId)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
                var roles = await _userManager.GetRolesAsync(user!);
                return roles != null ? roles.ToArray() : null!;
            }
            catch(Exception ex)
            {
                // _logger.LogError(//Message);
                // throw;
            }
            return null!;
        }

        public async Task<ApplicationUser?> GetUserNameAsync(string username)
        {
            try
            {
                return await _userManager.FindByEmailAsync(username);
            }
            catch(Exception ex)
            {
                // _logger.LogError(//Message);
                // throw;
            }
            return null!;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        
    }
}
