using System.Collections.Generic;
using System.Security.Claims;

namespace CrossCutting.Security.Identity
{
    public interface IAuthorization
    {
        /// <summary>
        /// Current entity (user) Identity
        /// </summary>
        ClaimsPrincipal ClaimsPrincipal { get; }

        /// <summary>
        /// Gets the user list of claims
        /// </summary>
        IEnumerable<Claim> Claims { get; }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated
        /// </summary>
        bool UserIsAuthenticated { get; }

        /// <summary>
        /// Gets the current user unique identifier
        /// </summary>
        int UserId { get; }

        /// <summary>
        /// Get the current user username claim
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the specified role
        /// </summary>
        /// <param name="role">Role name</param>
        /// <returns><value>True</value> if user is in role, <value>False</value> otherwise</returns>
        bool IsInRole(string role);

        /// <summary>
        /// Gets a claim value by it's type name
        /// </summary>
        /// <param name="claimTypeName">Claim type name</param>
        /// <returns>Claim value if found, <value>null</value> otherwise</returns>
        string GetClaim(string claimTypeName);

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the "sa" role
        /// </summary>
        /// <returns><value>True</value> if user is in manager role, <value>False</value> otherwise</returns>
        bool UserIsSuperAdmin();

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the admin role
        /// </summary>
        /// <returns><value>True</value> if user is in "admin", <value>False</value> otherwise</returns>
        bool UserIsAdmin();

        /// <summary>
        /// Returns a value that indicates whether the entity (user) represented
        /// by this claims principal is in the User role - an application user
        /// </summary>
        /// <returns><value>True</value> if user is in user role, <value>False</value> otherwise</returns>
        bool UserIsUser();
    }

}