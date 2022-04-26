using Autofac;
using Business.Core.Data;
using Business.Core.Data.Interfaces;
using CrossCutting.SearchFilters.DataAccess;
using CrossCutting.SearchFilters.DataAccess.Npgsql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System;
using System.Data;

namespace Data.AccessObjects
{
    /// <summary>
    /// Inject the IdentityServer service and AuthConfig Settings
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public class DefaultDataModule : Module
    {
        private bool _isDevelopment;
        private readonly IConfiguration _configuration;

        public DefaultDataModule()
        {
            _isDevelopment = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(Environments.Development));
        }

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
            builder.Register(db => new NpgsqlConnection(_configuration.GetConnectionString("PostgreSQL"))).As<IDbConnection>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionManager>().As<ITransactionManager>().InstancePerLifetimeScope();
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
