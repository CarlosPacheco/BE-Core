using CrossCutting.Security.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.Extensions.Logging;

namespace CrossCutting.Security.EventsType
{
    public class SignalRValidation : JwtBearerEvents
    {
        private readonly IIdentityServer _identityServer;
        private readonly ILogger<SignalRValidation> _logger;

        public SignalRValidation(IIdentityServer identityServer, ILogger<SignalRValidation> logger)
        {
            _identityServer = identityServer;
            _logger = logger;
        }

        public override Task MessageReceived(MessageReceivedContext context)
        {
            if (context.Request.Path.ToString().Contains("hub", StringComparison.InvariantCultureIgnoreCase) && context.Request.Query.TryGetValue(OidcConstants.TokenTypes.AccessToken, out StringValues token))
            {
                context.Token = token;
            }

            return Task.CompletedTask;
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
