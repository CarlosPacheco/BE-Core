using CrossCutting.Security.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CrossCutting.Security.EventsType
{
    public class UserValidation : JwtBearerEvents
    {
        private readonly IIdentityServer _identityServer;
        private readonly ILogger<UserValidation> _logger;

        public UserValidation(IIdentityServer identityServer, ILogger<UserValidation> logger)
        {
            _identityServer = identityServer;
            _logger = logger;
        }

        public override async Task TokenValidated(TokenValidatedContext context)
        {
            try
            {
                string accessToken = (context.SecurityToken as JwtSecurityToken).RawData;

                IEnumerable<Claim> claims = await _identityServer.GetUserInfoAsync(accessToken);
                context.Principal.Identities.First().AddClaims(claims);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "GetUserInfoAsync fail");
                throw;
            }
        }
    }
}
