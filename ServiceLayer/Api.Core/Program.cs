using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Api.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())
             .ConfigureAppConfiguration((hostingContext, config) =>
             {
                 config.AddJsonFile("appsettings.json")
#if PROD
      .AddJsonFile("appsettings.Production.json", optional: false, reloadOnChange: true);
#else // DEBUG
       .AddJsonFile("appsettings.Development.json");
#endif
             })
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
