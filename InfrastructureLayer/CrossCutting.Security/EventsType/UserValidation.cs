using CrossCutting.Security.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        public UserValidation(IIdentityServer identityServer)
        {
            _identityServer = identityServer;
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
