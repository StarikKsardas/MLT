using Microsoft.Extensions.DependencyInjection;
using MLT.Infrastructure.RepositoryMapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Infrastructure.Di
{
    public static class DiAutoMapper
    {
        public static IServiceCollection AddDiAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(RepositoryMappingProfile));
            return services;
        }
    }
}
