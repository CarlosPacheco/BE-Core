using IdentityModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CrossCutting.Security.Identity
{
    public class Authorization : IAuthorization
    {
        /// <summary>
        /// Current entity (user) Identity
        /// </summary>
        public ClaimsPrincipal User { get; protected set; }

        public Authorization(IHttpContextAccessor httpContextAccessor)
        {
            User = httpContextAccessor.HttpContext.User;
        }

        /// <summary>
        /// Gets the entity (user) list of claims
        /// </summary>
        public IEnumerable<Claim> Claims => User.Claims;

        /// <summary>
        /// Get the current user PreferredUserName claim
        /// </summary>
        public string UserName => User.FindFirstValue(JwtClaimTypes.PreferredUserName);

        /// <summary>
        /// Get the current user name claim (full name)
        /// End-User's full name in displayable form including all name parts, possibly including
        /// titles and suffixes, ordered according to the End-User's locale and preferences.
        /// </summary>
        public string Name => User.FindFirstValue(JwtClaimTypes.Name);

        /// <summary>
        /// Gets the current user unique identifier
        /// </summary>
        public string UserId => User.FindFirstValue(JwtClaimTypes.Subject);

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated
        /// </summary>
        public bool IsAuthenticated => User.Identity.IsAuthenticated;

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the "sa" role
        /// </summary>
        /// <returns><value>True</value> if user is in manager role, <value>False</value> otherwise</returns>
        public bool IsSuperAdmin => IsInRole(Roles.SuperAdministrator);

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the "admin" role
        /// </summary>
        /// <returns><value>True</value> if user is in admin role, <value>False</value> otherwise</returns>
        public bool IsAdmin => IsInRole(Roles.Administrator);

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the User role - an application user
        /// </summary>
        /// <returns><value>True</value> if user is in "user" role, <value>False</value> otherwise</returns>
        public bool IsUser => Enum.TryParse(User.FindFirstValue(ClaimTypes.UserType), true, out UserType userType) && userType.HasFlag(UserType.User);

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the specified role
        /// </summary>
        /// <param name="role">Role name</param>
        /// <returns><value>True</value> if user is in role, <value>False</value> otherwise</returns>
        public bool IsInRole(string role)
        {
            return User.IsInRole(role);
        }
    }
}
