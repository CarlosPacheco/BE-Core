using CrossCutting.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Npgsql.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use Cors Policy (Custom)
        /// </summary>
        /// <param name="appBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseCors(ServiceCollectionExtensions.CorsPolicyName);
            return appBuilder;
        }

        /// <summary>
        /// Add AddNpgsqlLogManager
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IApplicationBuilder AddNpgsqlLogManager(this IApplicationBuilder appBuilder)
        {
            NpgsqlLogManager.Provider = new SerilogNpgsqlLoggingProvider(appBuilder.ApplicationServices.GetService<ILoggerFactory>());
            NpgsqlLogManager.IsParameterLoggingEnabled = true;

            return appBuilder;
        }
    }
}
