using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.Json.Serialization;
using Application;
using Application.ObjectMapping;
using Autofac;
using AutoMapper;
using CrossCutting.Binders.TypeConverters;
using CrossCutting.Configurations;
using CrossCutting.IoC.Extensions;
using CrossCutting.SearchFilters.DataAccess;
using CrossCutting.Web.Extensions;
using Data.Mapping.Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Api.Core
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            SwaggerOptions swaggerOptions = Configuration.GetSection(SwaggerOptions.Key).Get<SwaggerOptions>();
            CorsOptions corsOptions = Configuration.GetSection(CorsOptions.Key).Get<CorsOptions>();

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            services.Configure<SwaggerOptions>(Configuration.GetSection(SwaggerOptions.Key));
            services.Configure<UploadedOptions>(Configuration.GetSection(UploadedOptions.key));
            services.Configure<CorsOptions>(Configuration.GetSection(CorsOptions.Key));

            // CORS Settings
            services.AddCorsPolicy(corsOptions);

            //load here other external dll controllers classes (before the AddMvc/Routing)
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-2.2#use-routing-middleware

            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddHttpContextAccessor();

            // Swagger UI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerOptions.Version, new OpenApiInfo { Title = swaggerOptions.Title, Version = swaggerOptions.Version });
            });

            // IoC Logger 
            services.AddSerilog(Configuration); //Configuration.GetSection("Logging").GetValue<string>("FilePath"));

            // Configure object mapping (AutoMapper), Get the assembly, AutoMapper will scan our assembly and look for classes that inherit from Profile
            services.AddAutoMapper(typeof(MappingProfile));

            // new one per DI request
            services.AddTransient<IDbConnection>(db => new SqlConnection(Configuration.GetConnectionString("SqlServer")));
        }

        /// <summary>
        /// Autofac Dependency Injection
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DefaultApplicationModule(Environment.EnvironmentName == "Development", Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IMapper autoMapper, ILogger logger, IOptions<SwaggerOptions> swaggerOptionsConfig)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.2#order
            //app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerOptionsConfig.Value.JsonRoute, swaggerOptionsConfig.Value.EndpointName);
                c.RoutePrefix = swaggerOptionsConfig.Value.RoutePrefix;
            });

            autoMapper.ConfigurationProvider.AssertConfigurationIsValid();

            SqlTypeMapper.SetupTypesMappingAndHandlers();

            // Type Descriptors
            TypeDescriptor.AddAttributes(typeof(DateTime), new TypeConverterAttribute(typeof(UtcDateTimeConverter)));

        }
    }
}
