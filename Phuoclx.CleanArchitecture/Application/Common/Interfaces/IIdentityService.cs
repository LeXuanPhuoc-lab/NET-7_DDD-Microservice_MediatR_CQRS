using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> CreateAsync(ApplicationUser user, string password, string role);
        Task<string?> GetUserNameAsync(string userId);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<IEnumerable<string>> GetRolesByUserIdAsync(string userId);

        //Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        //Task<Result> DeleteUserAsync(string userId);
    }
}
