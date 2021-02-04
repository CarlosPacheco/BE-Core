using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace CrossCutting.Helpers.Helpers
{
    /// <summary>
    /// Helper class for SQL Server related operations.
    /// </summary>
    public static class SqlHelper
    {
        public static DbCommand CreateDbCommand(string cmdText, DbConnection connection)
        {
            DbCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = cmdText;

            return command;
        }

        public static SqlCommand CreateDbCommand(string cmdText, SqlConnection connection)
        {
            return new SqlCommand(cmdText, connection);
        }

        #region Parameters Handling

        /// <summary>
        /// Creates a new SqlParameter with Output as it's ParameterDirection.
        /// </summary>
        /// <param name="parameterName">The SQL parameter name.</param>
        /// <param name="parameterType">The SQL parameter type.</param>
        /// <returns>A new</returns>
        public static SqlParameter OutputParameter(string parameterName, SqlDbType parameterType)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentException("parameterName is null or empty");
            }

            parameterName = GetCorrectedParameterName(parameterName);

            SqlParameter outputParam = new SqlParameter(parameterName, parameterType)
            {
                Direction = ParameterDirection.Output
            };

            return outputParam;
        }

        #endregion

        #region Full Text Search

        public static string GetCustomFullTextSearchQueryUsingNearPredicate(string searchQuery, bool useWordsAsSuffixes = false, int proximityMaxDistance = 10, bool matchOrder = false)
        {
            string _wordAsSufffixFormatTemplate = "{0}*";
            string _wordAsSimpleTermFormatTemplate = "{0}";
            string _multipleWordQueryFormatTemplate = "(\"{0}\") OR (NEAR(({1}), {2}, {3}))";
            string fullTextSearchExpression;

            searchQuery = searchQuery.Trim();

            List<string> queryWords = searchQuery.Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => string.Format(useWordsAsSuffixes ? _wordAsSufffixFormatTemplate : _wordAsSimpleTermFormatTemplate, s))
                .ToList();

            if (queryWords.Count == 0)
            {
                return null;
            }

            if (queryWords.Count > 1)
            {
                string nearTerms = string.Join(",", queryWords.Select(w => string.Format("\"{0}\"", w)));
                string literalTerms = string.Join(",", queryWords);

                fullTextSearchExpression = string.Format(_multipleWordQueryFormatTemplate, literalTerms, nearTerms, proximityMaxDistance, matchOrder.ToString().ToUpper());
            }
            else
            {
                fullTextSearchExpression = $"\"{queryWords[0]}\"";
            }

            return fullTextSearchExpression;
        }

        #endregion

        #region (Private) Auxiliary

        /// <summary>
        /// Adds the '@' char to the beginning of the parameter name if it's missing.
        /// </summary>
        /// <param name="parameterName">The parameter name.</param>
        /// <returns>Returns the corrected parameter name.</returns>
        private static string GetCorrectedParameterName(string parameterName)
        {
            if (parameterName.Length > 0 && '@' != parameterName[0])
            {
                parameterName = "@" + parameterName;
            }

            return parameterName;
        }

        #endregion
    }
}
