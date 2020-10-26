using Autofac;
using Business.Core;
using CrossCutting.Security.Identity;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Reflection;

namespace Business
{
    /// <summary>
    /// Autofac .NETCore Dependency Injection 
    /// Inject the IdentityServer service and AuthConfig Settings
    /// More information about the lifetimes (Transient/Scoped/Singleton):
    /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2#service-lifetimes
    /// </summary>
    public class DefaultBusinessModule : Autofac.Module
    {
        private bool _isDevelopment;
        private readonly IConfiguration _configuration;

        private const string DataAccessObjectsDdlName = "Data.AccessObjects";

        public DefaultBusinessModule()
        {
            _isDevelopment = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("Development"));
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
            builder.RegisterType<Authorization>().As<IAuthorization>();
            builder.RegisterAssemblyModules(Assembly.Load(DataAccessObjectsDdlName));
            builder.Register(c => new LoggerConfiguration().ReadFrom.Configuration(_configuration).CreateLogger()).As<ILogger>().SingleInstance();
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
