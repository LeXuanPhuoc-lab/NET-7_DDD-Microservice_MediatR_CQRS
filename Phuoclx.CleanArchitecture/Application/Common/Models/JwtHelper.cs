using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class JwtHelper
    {
        private readonly AppSettings _appSettings;
        private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(8); 
        public JwtHelper(AppSettings appSettings) 
        {
            _appSettings = appSettings; 
        }

        public string GenerateToken(ApplicationUser user, params string[] roles) 
        {
            // Get and encrypt secret key
            var secretKey = _appSettings.Key; 
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey); 

            // Token Handler 
            var tokenHandler = new JwtSecurityTokenHandler();

            //var claims = new List<Claim>()
            //{
            //    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new(JwtRegisteredClaimNames.Email, user.Email!),
            //    new(JwtRegisteredClaimNames.Sub, user.Email!),
            //    new("admin", user.Role),
            //    new("manager", user.Role),
            //    new("userId", user.Id)
            //};

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.Email!),
                new Claim("userId", Guid.NewGuid().ToString()),
            };
            if(roles != null)
            {
                foreach (string roleClaim in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleClaim));
                }
            }
            
            // Token Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                // Token details (email, role, username, id ...)
                Subject = new ClaimsIdentity(claims),
                //Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifeTime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha512)
            };

            // Generate token with descriptor 
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
