using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Application;
using Autofac;
using AutoMapper;
using CrossCutting.Binders.TypeConverters;
using CrossCutting.Configurations;
using CrossCutting.Extensions;
using CrossCutting.Security;
using CrossCutting.Security.EventsType;
using CrossCutting.Web.Extensions;
using CrossCutting.Web.JsonConverters;
using CrossCutting.Mapping.Dapper;
using Data.TransferObjects.ObjectMapping.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Collections.Generic;
using CrossCutting.Security.Configurations;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Microsoft.Extensions.Http;
using CrossCutting.Web;
using System.Net.Http;

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
            // IoC Logger 
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

            //TODO:: add auth to the swagger, maybe change the config objs to singleton and remove the addoptions etc
            AuthConfig? authConfig = Configuration.GetSection(AuthConfig.Position).Get<AuthConfig>();
            services.AddSingleton(authConfig);

            SwaggerOptionsConfig? swaggerOptionsConfig = Configuration.GetSection(SwaggerOptionsConfig.Position).Get<SwaggerOptionsConfig>();
            CorsConfig? corsConfig = Configuration.GetSection(CorsConfig.Position).Get<CorsConfig>();

            // Add our Config object so it can be injected
            services.Configure<SwaggerOptionsConfig>(Configuration.GetSection(SwaggerOptionsConfig.Position))
            .Configure<UploadedConfig>(Configuration.GetSection(UploadedConfig.Position))
            .Configure<CorsConfig>(Configuration.GetSection(CorsConfig.Position));

            // CORS Settings
            services.AddCorsPolicy(corsConfig);

            //load here other external dll controllers classes (before the AddMvc/Routing)
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-2.2#use-routing-middleware

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.Converters.Add(new PointConverter());

                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                if (Environment.IsDevelopment())
                    options.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddIdentityServerService<UserValidation>(Environment, Configuration, Environment.IsDevelopment() ? new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            } : null);

            if (Environment.IsDevelopment())
                services.AddTransient<HttpMessageHandlerBuilder, DangerousAcceptAnyServerCertHttpMessageHandlerBuilder>();

            // Swagger UI
            string _swaggerName = "oauth2";

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swaggerOptionsConfig.Version, new OpenApiInfo { Title = swaggerOptionsConfig.EndpointName, Version = swaggerOptionsConfig.Version });

                options.AddSecurityDefinition(_swaggerName, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{authConfig.Authority}/connect/authorize"),
                            TokenUrl = new Uri($"{authConfig.Authority}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "rc_fourfriends", "Four Friends data" }
                            }
                        }
                    }
                });

                options.AddSecurityRequirement(
                   new OpenApiSecurityRequirement
                   {
                        {
                            new OpenApiSecurityScheme{
                                Reference = new OpenApiReference
                                {
                                    Id = _swaggerName,
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
                   });
            });

            // Configure object mapping (AutoMapper), Get the assembly, AutoMapper will scan our assembly and look for classes that inherit from Profile
            services.AddAutoMapper(typeof(MappingProfile));

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

                options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("172.28.0.0"), 16));
                options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("209.126.11.207"), 16));
                options.KnownProxies.Add(IPAddress.Parse("209.126.11.207"));
                if (Environment.IsDevelopment())
                {
                    options.KnownNetworks.Clear();
                    options.KnownProxies.Clear();
                }
            });
        }

        /// <summary>
        /// Autofac Dependency Injection
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.Register(c => Log.Logger).As<ILogger>().SingleInstance();
            builder.RegisterModule(new DefaultApplicationModule(Environment.IsDevelopment(), Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IMapper autoMapper, IOptions<SwaggerOptionsConfig> swaggerOptionsConfig)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSerilogRequestLogging();
            }

            app.UseCustomExceptionMiddleware();
            app.UseHttpsRedirection();
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.2#order
            app.UseStaticFiles();
           
            app.UseSerilogRequestLogging();
            app.UseRouting();

            app.UseCorsPolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerOptionsConfig.Value.JsonRoute, swaggerOptionsConfig.Value.EndpointName);
                c.RoutePrefix = swaggerOptionsConfig.Value.RoutePrefix;
                c.OAuthClientId(swaggerOptionsConfig.Value.OidcSwaggerUIClientId);
                c.OAuthScopes("openid", "profile");
                c.OAuthAppName(swaggerOptionsConfig.Value.EndpointName);
                c.OAuthScopeSeparator(" ");
                c.OAuthUsePkce();
            });

            if (Environment.IsDevelopment())
                autoMapper.ConfigurationProvider.AssertConfigurationIsValid();

            SqlTypeMapper.SetupTypesMappingAndHandlers("Business");

            // Type Descriptors
            TypeDescriptor.AddAttributes(typeof(DateTime), new TypeConverterAttribute(typeof(UtcDateTimeConverter)));
        }
    }
}
