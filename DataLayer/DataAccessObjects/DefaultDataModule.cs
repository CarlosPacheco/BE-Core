using Autofac;
using Business.Core.Data.Interfaces;
using CrossCutting.SearchFilters.DataAccess;
using Microsoft.Extensions.Configuration;

namespace Data.AccessObjects
{
    /// <summary>
    /// Inject the IdentityServer service and AuthConfig Settings
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public class DefaultDataModule : Autofac.Module
    {
        private bool _isDevelopment;
        private readonly IConfiguration _configuration;

        public DefaultDataModule(bool isDevelopment, IConfiguration configuration)
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
            builder.RegisterAssemblyTypes(ThisAssembly).As<IBaseDao>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<PagedQueryBuilder>().As<IPagedQueryBuilder>().SingleInstance();
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
