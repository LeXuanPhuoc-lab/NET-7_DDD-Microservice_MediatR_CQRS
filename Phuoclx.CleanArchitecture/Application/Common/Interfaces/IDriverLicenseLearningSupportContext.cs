using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface IDriverLicenseLearningSupportContext
    {
        DbSet<Account> Accounts { get;}
        DbSet<Role> Roles { get; }
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
    }
}
