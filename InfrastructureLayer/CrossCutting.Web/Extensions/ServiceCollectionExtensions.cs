using CrossCutting.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CrossCutting.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public const string CorsPolicyName = "CorsPolicy";

        public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            var interfaces = typesFromAssemblies.Select(t => t.ImplementedInterfaces);
            foreach (var infc in interfaces)
                foreach (var type in infc)
                    services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
        }

        /// <summary>
        /// CORS Settings
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, CorsOptions corsOptions)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    builder =>
                    {
                        builder.WithOrigins(corsOptions.Origins)//.SetPreflightMaxAge(new TimeSpan(7, 0, 0, 0))
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .WithExposedHeaders("x-page-current", "x-page-size", "x-page-total", "content-disposition");
                    });
            });

            return services;
        }
    }
}
