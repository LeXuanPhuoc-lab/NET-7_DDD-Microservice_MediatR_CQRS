using Application.Common.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository.Cached;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultDB");

            services.AddDbContext<APIContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<DriverLicenseLearningSupportContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IDriverLicenseLearningSupportContext>(provider =>
                provider.GetRequiredService<DriverLicenseLearningSupportContext>());
            services.AddScoped<ApplicationDbContextInitialiser>();


            // Add Repository
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            // Scrutor
            services.Decorate<IIdentityRepository, CachedIdentityRepository>();


            return services;
        }
    }
}
