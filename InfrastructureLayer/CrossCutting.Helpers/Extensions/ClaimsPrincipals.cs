using System;
using System.Linq;
using System.Security.Claims;

namespace CrossCutting.Helpers.Extensions
{
    /// <summary>
    /// ClaimsPrincipals type Extension methods class.
    /// </summary>
    public static class ClaimsPrincipals
    {
        /// <summary>
        /// Gets a claim value by it's type name
        /// </summary>
        /// <param name="claimType">Claim type name</param>
        /// <returns>Claim value if found, <value>null</value> otherwise</returns>
        public static string GetClaim(this ClaimsPrincipal user, string claimType)
        {
            return user.Claims.SingleOrDefault(c => c.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase))?.Value;
        }
    }
}
