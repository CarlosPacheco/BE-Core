using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CrossCutting.Helpers.Extensions
{
    /// <summary>
    /// IdentityUserContext type Extension methods class.
    /// </summary>
    public static class IdentityUserContexts
    {
        /// <summary>
        /// Gets a claim value by it's type name
        /// </summary>
        /// <param name="claimType">Claim type name</param>
        /// <returns>Claim value if found, <value>null</value> otherwise</returns>
        public static async Task SetClaimAsync<TUser, TKey, TUserClaim, TUserLogin, TUserToken>(this IdentityUserContext<TUser, TKey, TUserClaim, TUserLogin, TUserToken> identityUserContext, TKey userId, string claimType, string claimValue) where TUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TUserToken : IdentityUserToken<TKey>
        {
            IdentityUserClaim<TKey> claimName = await identityUserContext.UserClaims.FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.ClaimType.Equals(claimType, StringComparison.OrdinalIgnoreCase));
            claimName.ClaimValue = claimValue;
        }

        /// <summary>
        /// Gets a claim value by it's type name
        /// </summary>
        /// <param name="claimType">Claim type name</param>
        /// <returns>Claim value if found, <value>null</value> otherwise</returns>
        public static async Task SetClaimAsync<TUser, TKey, TUserClaim, TUserLogin, TUserToken>(this IdentityUserContext<TUser, TKey, TUserClaim, TUserLogin, TUserToken> identityUserContext, IdentityUser<TKey> user, string claimType, string claimValue)
        where TUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TUserToken : IdentityUserToken<TKey>
        {
            IdentityUserClaim<TKey> claimName = await identityUserContext.UserClaims.FirstOrDefaultAsync(x => x.UserId.Equals(user.Id) && x.ClaimType.Equals(claimType, StringComparison.OrdinalIgnoreCase));
            claimName.ClaimValue = claimValue;
        }
    }
}
