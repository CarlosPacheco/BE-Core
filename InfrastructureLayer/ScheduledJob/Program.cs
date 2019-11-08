using System;
using System.Data;
using System.Reflection;
using System.Threading;
using CrossCutting.IoC.Extensions;
using CrossCutting.SearchFilters.DataAccess;
using CrossCutting.Web.IoC.Extensions;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oracle.ManagedDataAccess.Client;
using ScheduledJob.Services;
using Serilog;

namespace ScheduledJob
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create service collection and configure our services
            ServiceCollection serviceCollection = new ServiceCollection();

            //Setup the configuration file
            IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
#if PROD
      .AddJsonFile("appsettings.Production.json", optional: false, reloadOnChange: true);
#else // DEBUG
       .AddJsonFile("appsettings.Development.json")
#endif
       .Build();

            serviceCollection.AddSingleton(config => Configuration);

            SqlMapper.AddTypeMap(typeof(bool?), DbType.Int32);
            SqlMapper.AddTypeMap(typeof(bool), DbType.Int32);

            // IoC Logger 
            serviceCollection.AddSerilog(Configuration.GetSection("Logging").GetValue<string>("FilePath"));
            serviceCollection.AddScoped<IHandlerEFF, HandlerEFF>();
            serviceCollection.AddSingleton<IPagedQueryBuilder, PagedQueryBuilder>();
            serviceCollection.AddDependencyInjection(Assembly.Load("CrossCutting"), Assembly.Load("Business.LogicObjects"), Assembly.Load("Data.AccessObjects"));
            // new one per DI request
            serviceCollection.AddTransient<IDbConnection>(db => new OracleConnection(Configuration.GetConnectionString("AldaOracle")));

            // Generate a provider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IHandlerEFF service = serviceProvider.GetService<IHandlerEFF>();
            ILogger logger = serviceProvider.GetService<ILogger>();

            while (true)
            {
                try
                {
                    Business.Entities.ScheduledJob.ScheduledJob scheduledJob = service.GetById(1);
                    service.DeleteFilesTracking(scheduledJob);

                    service.ReadFiles(scheduledJob);

                    Thread.Sleep(scheduledJob.IntervalExecuted);//10 secs
                }
                catch (Exception ex)
                {
                    logger.Error(ex, ex.Message);
                }
            }

        }
    }
}
