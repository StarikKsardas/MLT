using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MLT.Infrastructure.Di;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MLT.Web.Contracts.WebModels;
using FluentValidation.AspNetCore;
using NLog;
using MLT.Web.Services.Rest.Middlwares;

namespace MLT.Web.Services.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserWeb>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MLT.Web.Services.Rest", Version = "v1" });
            });

            services.AddLogging(log => DiLogging.AddDiLogging(log, Configuration));


            DiMLTDbContext.AddDiMLTDbContext(services, Configuration);
            DiRepositories.AddDiRepositories(services);
            DiServices.AddDiServices(services);
            DiAutoMapper.AddDiAutoMapper(services);

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MLT.Web.Services.Rest v1"));
            }

            app.UseRouting();

            app.UseMiddleware<AuthorizationMiddleware>();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
