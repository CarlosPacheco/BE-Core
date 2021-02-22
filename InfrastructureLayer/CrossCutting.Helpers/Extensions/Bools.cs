using System;

namespace CrossCutting.Helpers.Extensions
{
    /// <summary>
    /// Bool data type Extension methods class.
    /// </summary>
    public static class Bools
    {

        /// <summary>
        /// If the <see cref="System.bool"/> is null, emtpy or white space only the special <see cref="DBNull"/> value is returned. 
        /// </summary>
        /// <param name="value">The target string.</param>
        /// <returns><see cref="DBNull"/> if the target string is null, empty or all white spaces else the current value is returned.</returns>
        public static object ToOracleNumber(this bool? value)
        {
            if (!value.HasValue)
            {
                return DBNull.Value;
            }

            return value.Value ? 1 : 0;
        }
    }
}
