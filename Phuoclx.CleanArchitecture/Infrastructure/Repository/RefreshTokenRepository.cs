using Application.Common.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly APIContext _context;

        public RefreshTokenRepository(APIContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetAsync(string jwtId)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.JwtId == jwtId);
        }

        public async Task<RefreshToken> GenerateRefreshTokenForUserAsync(ApplicationUser user, string tokenId)
        {
            // Generate refresh token
            var refreshToken = new RefreshToken()
            {
                Token = tokenId,
                JwtId = tokenId,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken;
        }
    }
}
