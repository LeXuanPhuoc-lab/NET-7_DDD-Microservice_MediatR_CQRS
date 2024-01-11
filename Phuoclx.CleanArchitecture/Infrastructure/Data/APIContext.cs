using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data;


//public class APIContextFactory : IDesignTimeDbContextFactory<APIContext>
//{
//    public APIContext CreateDbContext(string[] args)
//    {
//        var optionBuilder = new DbContextOptionsBuilder<APIContext>();
//        optionBuilder.UseSqlServer("Data Source=ASUSG513;Initial Catalog=DriverLicenseLearningSupport;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;User ID=sa;Password=12345");

//        return new APIContext(optionBuilder.Options);
//    }
//}


public class APIContext : IdentityDbContext<ApplicationUser>
{
    protected APIContext(DbContextOptions options) : base(options){ }

    // Entity extension 
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public APIContext(DbContextOptions<APIContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    //public Task<int> SaveChangeAsync(CancellationToken cancellationToken)
    //{
    //    return base.SaveChangesAsync(cancellationToken);
    //}
}
