using Autofac;
using Business.Core;
using CrossCutting.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Business
{
    /// <summary>
    /// Autofac .NETCore Dependency Injection 
    /// Inject the IdentityServer service and AuthConfig Settings
    /// More information about the lifetimes (Transient/Scoped/Singleton):
    /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2#service-lifetimes
    /// </summary>
    public class DefaultBusinessModule : Module
    {
        private bool _isDevelopment;
        private readonly IConfiguration _configuration;

        public DefaultBusinessModule()
        {
            _isDevelopment = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(Environments.Development));
        }

        public DefaultBusinessModule(bool isDevelopment, IConfiguration configuration)
        {
            _isDevelopment = isDevelopment;
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_isDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }
            RegisterCommonDependencies(builder);
        }

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly).AsClosedTypesOf(typeof(IBaseBlo<>)).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterModule(new DefaultSecurityModule(_isDevelopment, _configuration));
        }

        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add development only services
        }

        private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add production only services
        }
    }

}
