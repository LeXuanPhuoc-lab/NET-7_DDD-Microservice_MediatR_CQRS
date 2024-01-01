namespace Infrastructure.Repository
{
    public interface IIdentityRepository
    {
        Task<Result<ApplicationUser>> CreateAsync(ApplicationUser user, string password, string role);
        Task<ApplicationUser?> GetUserNameAsync(string username);
        Task<ApplicationUser?> GetByEmailAsync(string email);
        //Task<bool> IsInRoleAsync(string userId, string role);
        //Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<IEnumerable<string>> GetRolesbyUserIdAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetUsersAsync();
    }
}
