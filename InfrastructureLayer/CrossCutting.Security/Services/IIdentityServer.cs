using CrossCutting.Security.Models.User;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CrossCutting.Security.Services
{
    public interface IIdentityServer
    {
        public string AccessToken { get; set; }

        public Task<bool> UpdateUserAsync(UserDto user, string accessToken);

        public Task<IEnumerable<Claim>> GetUserInfoAsync(string accessToken);
    }
}
