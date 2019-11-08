using System;
using System.Linq;
using Dapper;

namespace CrossCutting.SearchFilters.DataAccess
{
    /// <summary>
    /// SQL Query build helper.
    /// </summary>
    public static class PagedQueryBuilder2
    {
        /// <summary>
        /// Template for paged dataset queries with 
        /// </summary>
        private static readonly string PagedFromAdHocQueryTemplate = @";
            WITH ResultQ
            AS 
            ({0})
            , 
            CountQ 
            AS
            ( SELECT COUNT(1) AS TotalRows FROM ResultQ )
            SELECT 
                ResultQ.*, CountQ.TotalRows
            FROM 
                ResultQ, CountQ
            ORDER BY
                {1}
            OFFSET {2} ROWS
            FETCH NEXT {3} ROWS ONLY
            OPTION(RECOMPILE)";

        private static readonly string SimplePagedQueryTemplate = @"
            {0} 
            /**orderby**/ 
            OFFSET {1} ROWS
            FETCH NEXT {2} ROWS ONLY
            OPTION(RECOMPILE)";

        /// <summary>
        /// Builds a query wrapper to retrieve a paged result set.
        /// </summary>
        /// <param name="sqlQuery">The get sql query to be paged</param>
        /// <param name="orderByClause">The full ORDER BY sql clause</param>
        /// <param name="offset">The records to start from when retrieving the result set</param>
        /// <param name="numberOfRows">The number of row to retrive in the data set</param>
        /// <returns>The specified number of rows, oredered as the order clause, starting from the offset value</returns>
        public static string PagedSqlQuery(string sqlQuery, string orderByClause, int offset, int numberOfRows)
        {
            string query = string.Format(PagedFromAdHocQueryTemplate, sqlQuery, orderByClause, offset, numberOfRows);
            return query;
        }

        /// <summary>
        /// Builds a query wrapper to retrieve a paged result set and it's total row count
        /// </summary>
        /// <param name="sqlQuery">The get sql query to be paged</param>
        /// <param name="orderByClauseMembers">The sql ORDER BY clause value. <example>Id DESC, Name ASC</example></param>
        /// <param name="rowOffset">The rows to skip when retrieving the result set</param>
        /// <param name="pageSize">The number of rows to retrive in the data set</param>
        /// <returns>The specified page rows, ordered</returns>
        public static string PagedQueryWithTotals(string sqlQuery, int rowOffset, int pageSize, string orderByClauseMembers = "Id")
        {
            SqlBuilder sqlBuilder = new SqlBuilder();

            if (!string.IsNullOrWhiteSpace(orderByClauseMembers))
            {
                string[] orderColumns = orderByClauseMembers.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string orderBy = string.Join(",", orderColumns.Select(c => $"Main_Q.{c}"));
                sqlBuilder.OrderBy(orderBy);
            }

            SqlBuilder.Template sqlTemplate = sqlBuilder.AddTemplate(string.Format(PagedFromAdHocQueryTemplate, sqlQuery, orderByClauseMembers, rowOffset, pageSize));

            return sqlTemplate.RawSql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tvfCall">The Table-Valued Function call expression. Must contain all the TVF required parameter names. <example>Func(@var1,@var2)</example></param>
        /// <param name="whereClause">The WHERE sql clause value</param>
        /// <param name="orderByClause">The ORDER BY sql clause value</param>
        /// <param name="offset">The records to start from when retrieving the result set</param>
        /// <param name="numberOfRows">The number of row to retrive in the data set</param>
        /// <returns>The specified number of rows, oredered as the order clause, starting from the offset value</returns>
        public static string PagedTvfResultQuery(string tvfCall, string whereClause, string orderByClause, int offset, int numberOfRows)
        {
            SqlBuilder sqlBuilder = new SqlBuilder();
            SqlBuilder.Template sqlTemplate = sqlBuilder.AddTemplate($@"SELECT * FROM {tvfCall} /**where**/ /**orderby**/");

            if (!string.IsNullOrWhiteSpace(whereClause))
            {
                sqlBuilder.Where(whereClause);
            }
            
            return PagedSqlQuery(sqlTemplate.RawSql, !string.IsNullOrWhiteSpace(orderByClause) ? orderByClause : "Id DESC", offset, numberOfRows);
        }

        public static string PagedQuery(string sql, int offset, int numberOfRows, string orderByClauseMembers = "Id")
        {
            SqlBuilder sqlBuilder = new SqlBuilder();
            SqlBuilder.Template sqlTemplate = sqlBuilder.AddTemplate(string.Format(SimplePagedQueryTemplate, sql, offset, numberOfRows));

            if (!string.IsNullOrWhiteSpace(orderByClauseMembers))
            {
                //string[] orderColumns = orderByClauseMembers.Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //string orderBy = string.Join(",", orderColumns.Select(c => c));
                sqlBuilder.OrderBy(orderByClauseMembers);
            }

            return sqlTemplate.RawSql;
        }
    }
}
