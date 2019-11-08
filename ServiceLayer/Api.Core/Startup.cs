using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.ObjectMapping;
using AutoMapper;
using CrossCutting.Binders.TypeConverters;
using CrossCutting.Configurations;
using CrossCutting.IoC.Extensions;
using CrossCutting.SearchFilters.DataAccess;
using CrossCutting.Web.Extensions;
using CrossCutting.Web.IoC.Extensions;
using Dapper;
using Data.Mapping.Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            SwaggerOptionsConfig swaggerOptionsConfig = new SwaggerOptionsConfig();
            Configuration.GetSection("SwaggerOptions").Bind(swaggerOptionsConfig);
            CorsConfig corsConfig = new CorsConfig();
            Configuration.GetSection("Cors").Bind(corsConfig);

            // CORS Settings
            services.AddCorsPolicy(corsConfig);

            //load here other external dll controllers classes (before the AddMvc/Routing)
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-2.2#use-routing-middleware

            services.AddMvc(options => { }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddHttpContextAccessor();

            // Swagger UI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerOptionsConfig.Version, new OpenApiInfo { Title = swaggerOptionsConfig.Title, Version = swaggerOptionsConfig.Version });
            });

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            services.Configure<SwaggerOptionsConfig>(Configuration.GetSection("SwaggerOptions"));
            services.Configure<UploadedConfig>(Configuration.GetSection("Uploaded"));
            services.Configure<CorsConfig>(Configuration.GetSection("Cors"));

            // IoC Logger 
            services.AddSerilog(Configuration.GetSection("Logging").GetValue<string>("FilePath"));

            // Configure object mapping (AutoMapper), Get the assembly, AutoMapper will scan our assembly and look for classes that inherit from Profile
            services.AddAutoMapper(typeof(MappingProfile));

            // IoC Dependency Injection
            services.AddDependencyInjection(Assembly.Load("Application"), Assembly.Load("CrossCutting"), Assembly.Load("Business.LogicObjects"), Assembly.Load("Data.AccessObjects"));

            // new one per DI request
            services.AddTransient<IDbConnection>(db => new SqlConnection(Configuration.GetConnectionString("SqlServer")));
            services.AddSingleton<IPagedQueryBuilder, PagedQueryBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper autoMapper, ILogger logger, IOptions<SwaggerOptionsConfig> swaggerOptionsConfig)
        {
            if (env.IsDevelopment())
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
