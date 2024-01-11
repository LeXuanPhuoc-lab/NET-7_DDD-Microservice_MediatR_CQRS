namespace Infrastructure.Repository
{
    public interface IIdentityRepository
    {
        Task<Result<ApplicationUser>> CreateAsync(ApplicationUser user, string password, string role);
        Task<ApplicationUser?> GetByIdAsync(string userId);
        Task<ApplicationUser?> GetUserNameAsync(string username);
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<IEnumerable<string>> GetRolesbyUserIdAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetUsersAsync();
    }
}
