using System.Reflection;
using Business.Core;
using CrossCutting.Security.Identity;
using CrossCutting.Web.Services;
using Data.Core;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace CrossCutting.Web.IoC.Extensions
{
    /// <summary>
    /// Native ASP.NET Core Dependency Injection 
    /// </summary>
    public static class NetCoreDi
    {
        /// <summary>
        /// Extension to add all Dependency Injection
        /// More information about the lifetimes (Transient/Scoped/Singleton):
        /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2#service-lifetimes
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies">The assemblies to should be scanned.</param>
        public static void AddDependencyInjection(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(IBaseBlo<>))).UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces().WithScopedLifetime()

                .AddClasses(classes => classes.AssignableTo<IBaseDao>()).UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces().WithTransientLifetime()

                .AddClasses(classes => classes.AssignableTo<IAuthorization>()).UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces().WithScopedLifetime()

                .AddClasses(classes => classes.AssignableTo(typeof(IBaseService<>))).UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces().WithTransientLifetime());
        }
    }
}
