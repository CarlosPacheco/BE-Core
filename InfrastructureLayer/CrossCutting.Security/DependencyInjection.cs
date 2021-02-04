using CrossCutting.Security.Configurations;
using CrossCutting.Security.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;

namespace CrossCutting.Security
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Inject the IdentityServer service and AuthConfig Settings
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityServerService<T>(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration config) where T : class
        {
            services.AddHttpClient();
            services.Configure<AuthConfig>(config.GetSection(AuthConfig.Position));
            services.AddScoped<IIdentityServer, IdentityServer>();
            AuthConfig authConfig = config.GetSection(AuthConfig.Position).Get<AuthConfig>();
            //services.AddHttpContextAccessor();
            //services.AddScoped<IAuthorization, Authorization>();

            if (environment.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
            }

            services.AddScoped<T>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                // base-address of your identityserver
                options.Authority = authConfig.Authority;
                options.SaveToken = true;

                // Verifica se um token recebido ainda é válido
                options.TokenValidationParameters.ValidateLifetime = true;

                options.EventsType = typeof(T);

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
                if (environment.IsDevelopment())
                {
                    //TODO: make true, it is false for development only
                    options.RequireHttpsMetadata = false;
                }
                // you don't validate the audience at all - instead you write an authorization policy to check for the existence of the scope eg:
                // services.AddAuthorization(options => options.AddPolicy("myscope", policy => policy.RequireClaim("scope", "foo")));
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
