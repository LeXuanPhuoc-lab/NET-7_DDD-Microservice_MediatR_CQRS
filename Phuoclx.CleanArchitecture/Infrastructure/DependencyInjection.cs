using Application.Common.Interfaces;
using Infrastructure.Data;
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

            return services;
        }
    }
}
