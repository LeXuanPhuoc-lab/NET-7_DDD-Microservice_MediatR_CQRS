using Application.Common.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultDB");

            services.AddDbContext<DriverLicenseLearningSupportContext>((sp, options) => {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IDriverLicenseLearningSupportContext>(provider => 
                provider.GetRequiredService<DriverLicenseLearningSupportContext>());

            return services;
        }
    }
}
