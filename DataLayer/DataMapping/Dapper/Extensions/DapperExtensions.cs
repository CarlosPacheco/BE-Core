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
        /// <typeparam name="TParent">Parent data type</typeparam>
        /// <typeparam name="TChild">Children data type</typeparam>
        /// <typeparam name="TKey">Keys data type</typeparam>
        /// <param name="reader"><see cref="GridReader"/> instance</param>
        /// <param name="firstKey">Parent key get Func</param>
        /// <param name="secondParentKey">Child key get Func</param>
        /// <param name="childSelector">Child list manipulation Action</param>
        /// <returns>A list of <typeparamref name="TParent"/> objects with mapped children</returns>
        public static IEnumerable<TParent> QueryOneToMany<TParent, TChild, TKey>(this GridReader reader, Func<TParent, TKey> firstKey, Func<TChild, TKey> secondParentKey, Action<TParent, IEnumerable<TChild>> childSelector)
        {
            List<TParent> first = reader.Read<TParent>().ToList();
            Dictionary<TKey, IEnumerable<TChild>> childMap = reader.Read<TChild>().GroupBy(secondParentKey).ToDictionary(g => g.Key, g => g.AsEnumerable());

            foreach (TParent item in first)
            {
                if (childMap.TryGetValue(firstKey(item), out IEnumerable<TChild> children))
                {
                    childSelector(item, children);
                }
            }

            return first;
        }

        public static IEnumerable<TParent> QueryParentChild<TParent, TChild, TParentKey>(this IDbConnection connection, string sql, Func<TParent, TParentKey> parentKeySelector, Func<TParent, IList<TChild>> childSelector, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            Dictionary<TParentKey, TParent> cache = new Dictionary<TParentKey, TParent>();

            connection.Query<TParent, TChild, TParent>(sql,
                (parent, child) =>
                {
                    if (!cache.ContainsKey(parentKeySelector(parent)))
                    {
                        cache.Add(parentKeySelector(parent), parent);
                    }

                    TParent cachedParent = cache[parentKeySelector(parent)];
                    IList<TChild> children = childSelector(cachedParent);
                    children.Add(child);
                    return cachedParent;
                },
                param, transaction, buffered, splitOn, commandTimeout, commandType);

            return cache.Values;
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
