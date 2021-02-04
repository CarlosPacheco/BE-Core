namespace CrossCutting.Security
{
    /// <summary>
    /// Generally available roles
    /// </summary>
    public sealed class Roles
    {
        /// <summary>
        /// Converts a list of multiple roles into a string of comma separated values
        /// </summary>
        /// <param name="roles">The multiple roles</param>
        /// <returns>A string of roles separated by a comma</returns>
        public static string AsCsv(params string[] roles)
        {
            return string.Join(@",", roles);
        }

        private const string RolePrefix = "auth.";

        public const string Administrator = RolePrefix + "admin";

        public const string SuperAdministrator = "sa";
        public const string Read = "read";
        public const string Write = "write";
        public const string Delete = "delete";
        
    }
}
