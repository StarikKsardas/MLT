using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;


namespace MLT.Infrastructure.Di
{
    public static class DiLogging
    {
        public static ILoggingBuilder AddDiLogging(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
        {           
            //Need change OracleDbConnection after build!!!
            loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);            
            loggingBuilder.AddNLog("Helpers/nlog.config");            
            return loggingBuilder;
        }
    }
}
