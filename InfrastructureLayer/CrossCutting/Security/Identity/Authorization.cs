using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CrossCutting.Security.Identity
{
    public class Authorization : IAuthorization
    {
        /// <summary>
        /// Current entity (user) Identity
        /// </summary>
        public ClaimsPrincipal ClaimsPrincipal { get; protected set; }

        public Authorization()
        { 
        }

        public Authorization(IHttpContextAccessor httpContextAccessor)
        {
            ClaimsPrincipal = httpContextAccessor.HttpContext.User;
        }

        /// <summary>
        /// Gets the entity (user) list of claims
        /// </summary>
        public IEnumerable<Claim> Claims => ClaimsPrincipal.Claims;

        /// <summary>
        /// Get the current user username claim
        /// </summary>
        public string UserName => GetClaim(ClaimTypes.Username);

        /// <summary>
        /// Gets the current user unique identifier
        /// </summary>
        public int UserId => Convert.ToInt32(ClaimsPrincipal.FindFirst("sub").Value);

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated
        /// </summary>
        public bool UserIsAuthenticated => ClaimsPrincipal.Identity.IsAuthenticated;

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the specified role
        /// </summary>
        /// <param name="role">Role name</param>
        /// <returns><value>True</value> if user is in role, <value>False</value> otherwise</returns>
        public bool IsInRole(string role)
        {
            return ClaimsPrincipal.IsInRole(role);
        }

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the "sa" role
        /// </summary>
        /// <returns><value>True</value> if user is in manager role, <value>False</value> otherwise</returns>
        public bool UserIsSuperAdmin()
        {
            return IsInRole(Roles.SuperAdministrator);
        }

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the "admin" role
        /// </summary>
        /// <returns><value>True</value> if user is in admin role, <value>False</value> otherwise</returns>
        public bool UserIsAdmin()
        {
            return IsInRole(Roles.Administrator);
        }
        
        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the User role - an application user
        /// </summary>
        /// <returns><value>True</value> if user is in "user" role, <value>False</value> otherwise</returns>
        public bool UserIsUser()
        {
            return Enum.TryParse(GetClaim(ClaimTypes.UserType), true, out UserType userType) && userType.HasFlag(UserType.User);
        }
        
        /// <summary>
        /// Gets a claim value by it's type name
        /// </summary>
        /// <param name="claimTypeName">Claim type name</param>
        /// <returns>Claim value if found, <value>null</value> otherwise</returns>
        public string GetClaim(string claimTypeName)
        {
            return Claims.SingleOrDefault(c => c.Type.Equals(claimTypeName, StringComparison.OrdinalIgnoreCase))?.Value;
        }
    }
}
