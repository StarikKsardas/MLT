using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MLT.Data.Repositories.DatabaseContext;

namespace MLT.Infrastructure.Di
{
    public static class DiDactoDbContext
    {
        public static IServiceCollection AddDiDactoDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DactoDbContext>(options =>
            {
                options.UseOracle(configuration.GetConnectionString("DactoDBConnection"), options =>
                { options.UseOracleSQLCompatibility("11"); });
            });

            return services;
        }
    }
}
