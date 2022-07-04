using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MLT.Data.Repositories.DatabaseContext;


namespace MLT.Infrastructure.Di
{ 
    public static class DiMLTDbContext
    {
        public static IServiceCollection AddDiMLTDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MLTDbContext>(options =>
            {
                options
                .UseLazyLoadingProxies()
                .UseOracle(configuration.GetConnectionString("MLTDBConnection"), options =>
                { options.UseOracleSQLCompatibility("11");});
            });

            return services;
        }
    }
}

