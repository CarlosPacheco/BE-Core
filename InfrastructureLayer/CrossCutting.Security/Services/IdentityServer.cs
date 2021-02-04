using CrossCutting.Security.Models.User;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using CrossCutting.Helpers.Extensions;
using CrossCutting.Security.Configurations;
using Microsoft.Extensions.Options;

namespace CrossCutting.Security.Services
{
    public class IdentityServer : IIdentityServer
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AuthConfig _authConfig;

        private static DiscoveryCache Cache;

        public string AccessToken { get; set; }

        public IdentityServer(IHttpClientFactory httpClientFactory, IOptions<AuthConfig> authConfig)
        {
            _httpClientFactory = httpClientFactory;
            _authConfig = authConfig.Value;
            Cache = new DiscoveryCache(_authConfig.Authority);
        }

        private async Task GetToken()
        {
            if (!string.IsNullOrWhiteSpace(AccessToken)) return;

            DiscoveryDocumentResponse disco = await Cache.GetAsync();
            if (disco.IsError) throw new Exception(disco.Error);

            HttpClient tokenClient = _httpClientFactory.CreateClient();
            TokenResponse tokenResponse = await tokenClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = _authConfig.ClientId,
                ClientSecret = _authConfig.ClientSecret
            });

            if (tokenResponse.IsError) throw new Exception(tokenResponse.Error);

            AccessToken = tokenResponse.AccessToken;
        }

        public async Task<bool> UpdateUserAsync(UserDto user, string userId)
        {
            if(string.IsNullOrWhiteSpace(AccessToken)) await GetToken();

            // call API
            HttpClient identityServerClient = _httpClientFactory.CreateClient();
            identityServerClient.SetBearerToken(AccessToken);

            HttpResponseMessage response = await identityServerClient.PutAsync($"{_authConfig.Authority}/api/users/{userId}", user);

            return (response.IsSuccessStatusCode);
        }

        public async Task<IEnumerable<Claim>> GetUserInfoAsync(string accessToken)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            DiscoveryDocumentResponse disco = await Cache.GetAsync();
            if (disco.IsError) throw new Exception(disco.Error);

            UserInfoResponse response = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = disco.UserInfoEndpoint,
                Token = accessToken
            });

            if (response.IsError) throw new Exception(response.Error);

            return response.Claims;
        }
    }
}
