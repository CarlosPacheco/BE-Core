using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CrossCutting.Configurations;

namespace Api.Core
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        private readonly SwaggerOptionsConfig _swaggerOptionsConfig;

        public AuthorizeCheckOperationFilter(IOptions<SwaggerOptionsConfig> swaggerOptionsConfig)
        {
            _swaggerOptionsConfig = swaggerOptionsConfig.Value;
        }
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            bool hasAuthorize = context.MethodInfo.DeclaringType != null && (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any());

            if (hasAuthorize)
            {
                operation.Responses.Add(StatusCodes.Status401Unauthorized.ToString(), new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add(StatusCodes.Status403Forbidden.ToString(), new OpenApiResponse { Description = "Forbidden" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecurityScheme {Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "oauth2"}
                            }
                        ] = new[] { _swaggerOptionsConfig.OidcApiName }
                    }
                };

            }
        }
    }
}

/*
     "AdminApiConfiguration": {
        "ApiName": "Skoruba IdentityServer4 Admin Api",
        "ApiVersion": "v1",
        "ApiBaseUrl": "https://localhost:44302",
        "IdentityServerBaseUrl": "https://localhost:44310",
        "OidcSwaggerUIClientId": "skoruba_identity_admin_api_swaggerui",
        "OidcApiName": "skoruba_identity_admin_api",
        "AdministrationRole": "SkorubaIdentityAdminAdministrator",
        "RequireHttpsMetadata": false,
        "CorsAllowAnyOrigin": true,
        "CorsAllowOrigins": []
    },
 */