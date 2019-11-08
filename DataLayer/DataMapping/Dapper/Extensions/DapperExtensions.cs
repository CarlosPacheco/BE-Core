using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Dapper;
using Data.Mapping.Dapper.Oracle;
using static Dapper.SqlMapper;

namespace Data.Mapping.Dapper.Extensions
{
    public static class DapperExtensions
    {
        /// <summary>
        /// One to many (1 -> *) multi-map.
        /// Load a list of children to a parent's property.
        /// </summary>
        /// <typeparam name="TFirst">Parent data type</typeparam>
        /// <typeparam name="TSecond">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="reader"><see cref="SqlMapper.GridReader"/> instance</param>
        /// <param name="firstKey">Parent key get Func</param>
        /// <param name="secondKey">Child key get Func</param>
        /// <param name="addChildren">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TFirst"/> objects with mapped children</returns>
        public static IEnumerable<TFirst> Map<TFirst, TSecond, TKey>(this SqlMapper.GridReader reader, Func<TFirst, TKey> firstKey, Func<TSecond, TKey> secondKey, Action<TFirst, IEnumerable<TSecond>> addChildren)
        {
            List<TFirst> first = reader.Read<TFirst>().ToList();
            Dictionary<TKey, IEnumerable<TSecond>> childMap = reader
                .Read<TSecond>()
                .GroupBy(secondKey)
                .ToDictionary(g => g.Key, g => g.AsEnumerable());

            foreach (TFirst item in first)
            {
                if (childMap.TryGetValue(firstKey(item), out IEnumerable<TSecond> children))
                {
                    addChildren(item, children);
                }
            }

            return first;
        }

        /// <summary>
        /// Execute the query and get the new unique identifier
        /// </summary>
        /// <param name="cnn">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The number of the new unique identifier.</returns>
        public static T ExecuteIdOracle<T>(this IDbConnection cnn, string sql, object param, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            string insertSql = $@"/* Create */              
            {sql}
            returning Id into :Id";

            DynamicParameters dynamicParameters = new DynamicParameters(param);

            dynamicParameters.Add(name: "Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            cnn.Execute(insertSql, dynamicParameters, transaction, commandTimeout, commandType);

            return dynamicParameters.Get<T>("Id"); // Get value (new unique identifier)
        }

        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="cnn">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction"> The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns></returns>
        public static GridReader QueryMultipleOracle(this IDbConnection cnn, string sql, object param, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            string pattern = @"(?:SELECT|INSERT)[\s\S]*?(?=\""|\;)";

            string oracleSql = "BEGIN";

            OracleDynamicParameters oracleParam = new OracleDynamicParameters(param);
            foreach (Match m in Regex.Matches(sql, pattern, RegexOptions.Multiline))
            {
                oracleSql += $" OPEN :cursor{m.Index} FOR {m.Value};";
                oracleParam.AddRefCursorParameters($"cursor{m.Index}");
            }

            oracleSql += " END;";

            return cnn.QueryMultiple(oracleSql, oracleParam, transaction, commandTimeout, commandType);
        }
    }
}
