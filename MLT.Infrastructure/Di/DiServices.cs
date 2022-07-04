using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MLT.Domain.Contracts.Services;
using MLT.Domain.Services.Services;
using MLT.Infrastructure.RepositoryMapping;

namespace MLT.Infrastructure.Di
{
    public static class DiServices
    {
        public static IServiceCollection AddDiServices(this IServiceCollection services)
        {            
            services.AddTransient<IInformationService, InformationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProtectionService, ProtectionService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ILatentService, LatentService>();
            services.AddTransient<IQueryService, QueryService>();
            services.AddTransient<IMessageService, MessageService>();
            return services;
        }
    }
}
