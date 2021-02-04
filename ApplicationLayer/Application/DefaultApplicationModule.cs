using Autofac;
using Business;
using Business.Core.Services;
using Data.AccessObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Application
{
    /// <summary>
    /// Inject the IdentityServer service and AuthConfig Settings
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public class DefaultApplicationModule : Module
    {
        private bool _isDevelopment;
        private readonly IConfiguration _configuration;

        public DefaultApplicationModule()
        {
            _isDevelopment = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(Environments.Development));
        }

        public DefaultApplicationModule(bool isDevelopment, IConfiguration configuration)
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
            builder.RegisterAssemblyTypes(ThisAssembly).AsClosedTypesOf(typeof(IBaseService<>)).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterModule(new DefaultBusinessModule(_isDevelopment, _configuration));
            builder.RegisterModule(new DefaultDataModule(_isDevelopment, _configuration));
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
