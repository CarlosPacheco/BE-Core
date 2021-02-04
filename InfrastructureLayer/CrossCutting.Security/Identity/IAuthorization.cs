using System.Collections.Generic;
using System.Security.Claims;

namespace CrossCutting.Security.Identity
{
    public interface IAuthorization
    {
        /// <summary>
        /// Current entity (user) Identity
        /// </summary>
        ClaimsPrincipal User { get; }

        /// <summary>
        /// Gets the user list of claims
        /// </summary>
        IEnumerable<Claim> Claims { get; }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Gets the current user unique identifier
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// Get the current user username claim
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Get the current user name claim (full name)
        /// End-User's full name in displayable form including all name parts, possibly including
        ///  titles and suffixes, ordered according to the End-User's locale and preferences.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the "sa" role
        /// </summary>
        /// <returns><value>True</value> if user is in manager role, <value>False</value> otherwise</returns>
        bool IsSuperAdmin { get; }

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the admin role
        /// </summary>
        /// <returns><value>True</value> if user is in "admin", <value>False</value> otherwise</returns>
        bool IsAdmin { get; }

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the User role - an application user
        /// </summary>
        /// <returns><value>True</value> if user is in user role, <value>False</value> otherwise</returns>
        bool IsUser { get; }

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the specified role
        /// </summary>
        /// <param name="role">Role name</param>
        /// <returns><value>True</value> if user is in role, <value>False</value> otherwise</returns>
        bool IsInRole(string role);
    }

}