using Microsoft.Extensions.DependencyInjection;
using MLT.Data.Contracts.Repositories;
using MLT.Data.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Infrastructure.Di
{
    public static class DiRepositories
    {
        public static IServiceCollection AddDiRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IInformationRepository, InformationRepository>();
            services.AddTransient<ILatentRepository, LatentRepository>();
            services.AddTransient<IAnswerMobileRepository, AnswerMobileRepository>();
            services.AddTransient<IQueryRepository, QueryRepository>();
            return services;
        }
    }

}
