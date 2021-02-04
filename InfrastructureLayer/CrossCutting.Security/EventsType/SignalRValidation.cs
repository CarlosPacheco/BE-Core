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

namespace CrossCutting.Security.EventsType
{
    public class SignalRValidation : JwtBearerEvents
    {
        private readonly IIdentityServer _identityServer;

        public SignalRValidation(IIdentityServer identityServer)
        {
            _identityServer = identityServer;
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
