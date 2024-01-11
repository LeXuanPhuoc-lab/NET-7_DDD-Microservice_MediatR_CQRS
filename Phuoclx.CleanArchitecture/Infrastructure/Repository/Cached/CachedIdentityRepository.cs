using LanguageExt.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Cached
{
    public class CachedIdentityRepository : IIdentityRepository
    {
        private readonly IIdentityRepository _identityRepository; // IdentityRepository
        private readonly ConcurrentDictionary<Guid, ApplicationUser> _cache = new();


        public CachedIdentityRepository(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task<Result<ApplicationUser>> CreateAsync(ApplicationUser user, string password, string role)
        {
            return await _identityRepository.CreateAsync(user, password, role);
        }
        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _identityRepository.GetByEmailAsync(email);
        }

        public async Task<ApplicationUser?> GetByIdAsync(string userId)
        {
            return await _identityRepository.GetByIdAsync(userId);
        }

        public async Task<IEnumerable<string>> GetRolesbyUserIdAsync(string userId)
        {
            return await _identityRepository.GetRolesbyUserIdAsync(userId);
        }

        public async Task<ApplicationUser?> GetUserNameAsync(string username)
        {
            return await _identityRepository.GetUserNameAsync(username);
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            if (_cache.Count > 0)
                return _cache.Values.ToList();

            var users = await _identityRepository.GetUsersAsync();
            foreach (var user in users)
                _cache.TryAdd(Guid.Parse(user.Id), user);
            return users; 
        }
    }
}
