using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MLT.Data.Contracts.Repositories;
using MLT.Domain.Contracts.Services;
using MLT.Infrastructure.Di;
using NLog;
using System;
using System.IO;
using System.Windows;

namespace MLT.Desktop.AppUsers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    

    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            

            var host = new HostBuilder().
                ConfigureServices((hostContext, services) =>
                {
                    ConfigureServices(services);
                }).ConfigureLogging(log =>
                {
                    ConfigureLogging(log);                    
                }).Build();

            LogManager.Configuration.Variables["DbConnectionString"] = Configuration.GetConnectionString("MLTDBConnection");

            ServiceProvider = host.Services.CreateScope().ServiceProvider;
                    
            var loginForm = ServiceProvider.GetRequiredService<LoginForm>();            
            loginForm.Show();
        }
        
        private void ConfigureServices(IServiceCollection services)
        {
            DiMLTDbContext.AddDiMLTDbContext(services, Configuration);
            DiRepositories.AddDiRepositories(services);
            DiServices.AddDiServices(services);
            DiAutoMapper.AddDiAutoMapper(services);

            services.AddTransient(typeof(LoginForm));
            services.AddTransient(typeof(AtdForm));
            services.AddTransient(typeof(MainForm));
            services.AddTransient(typeof(UserForm));            
        }

        private void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            DiLogging.AddDiLogging(loggingBuilder, Configuration);
        }
        
    }
}
