using Autofac;
using Business.Core;
using Business.Core.Services;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Application
{
    /// <summary>
    /// Inject the IdentityServer service and AuthConfig Settings
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public class DefaultApplicationModule : Autofac.Module
    {
        private bool _isDevelopment;
        private readonly IConfiguration _configuration;

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
            builder.RegisterAssemblyModules(Assembly.GetAssembly(typeof(IBaseBlo)));
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
