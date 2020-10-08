using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace CrossCutting.IoC.Extensions
{
    public static class Serilog
    {
        public static void AddSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton((ILogger)new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger());
        }
    }
}
