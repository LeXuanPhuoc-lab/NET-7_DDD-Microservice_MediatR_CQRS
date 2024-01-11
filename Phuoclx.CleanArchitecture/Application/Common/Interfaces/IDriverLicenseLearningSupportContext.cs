using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface IDriverLicenseLearningSupportContext
    {
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<Account> Accounts { get;}
        DbSet<Role> Roles { get; }
        DbSet<ApplicationUser> Users { get; }
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
    }
}
