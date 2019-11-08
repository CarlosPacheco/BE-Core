using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CrossCutting.IoC.Extensions
{
    public static class Serilog
    {
        public static void AddSerilog(this IServiceCollection services, string loggerPath)
        {
            services.AddSingleton((ILogger)new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.RollingFile(loggerPath).CreateLogger());
        }
    }
}
