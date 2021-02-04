using System;
using System.Linq;
using System.Text.RegularExpressions;
using Dapper;

namespace CrossCutting.SearchFilters.DataAccess.SqlServer
{
    /// <summary>
    /// SQL Query build helper.
    /// </summary>
    public partial class PagedQueryBuilderSqlServer : IPagedQueryBuilder
    {
        private const string SqlTotalCountSelectClauseValue = @"TotalCount = COUNT(1)";
        private readonly Regex SqlSelectClauseRegex = new Regex(@"(SELECT)(.*?)( FROM)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// Builds a query wrapper to retrieve a paged result set.
        /// </summary>
        /// <param name="sqlQuery">The get sql query to be paged</param>
        /// <param name="orderByClause">The full ORDER BY sql clause</param>
        /// <param name="offset">The records to start from when retrieving the result set</param>
        /// <param name="numberOfRows">The number of row to retrive in the data set</param>
        /// <returns>The specified number of rows, oredered as the order clause, starting from the offset value</returns>
        public virtual string PagedSqlQuery(string sqlQuery, string orderByClause, int offset, int numberOfRows)
        {
            return string.Format(CtePagingAndTotalCountQueryTemplate, sqlQuery, orderByClause, offset, numberOfRows);
        }

        /// <summary>
        /// Builds a query wrapper to retrieve a paged result set and it's total row count
        /// </summary>
        /// <param name="sqlQuery">The get sql query to be paged</param>
        /// <param name="orderByClauseMembers">The sql ORDER BY clause value. <example>Id DESC, Name ASC</example></param>
        /// <param name="rowOffset">The rows to skip when retrieving the result set</param>
        /// <param name="pageSize">The number of rows to retrive in the data set</param>
        /// <returns>The specified page rows, ordered</returns>
        public virtual string PagedQueryWithTotals(string sqlQuery, int rowOffset, int pageSize, string orderByClauseMembers = "Id")
        {
            SqlBuilder sqlBuilder = new SqlBuilder();

            if (!string.IsNullOrWhiteSpace(orderByClauseMembers))
            {
                string[] orderColumns = orderByClauseMembers.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string orderBy = string.Join(",", orderColumns.Select(c => $"Main_Q.{c}"));
                sqlBuilder.OrderBy(orderBy);
            }

            SqlBuilder.Template sqlTemplate = sqlBuilder.AddTemplate(string.Format(CtePagingAndTotalCountQueryTemplate, sqlQuery, orderByClauseMembers, rowOffset, pageSize));

            return sqlTemplate.RawSql;
        }

        public virtual string PagedQuery(string sqlQuery, ISearchFilter filter, ref object param)
        {
            SqlBuilder sqlBuilder = new SqlBuilder();
            SqlBuilder.Template sqlTemplate = sqlBuilder.AddTemplate(string.Format(PagingQueryTemplate, sqlQuery, filter.Offset, filter.PageSize));

            if (!string.IsNullOrWhiteSpace(filter.SqlOrderByExpression))
            {
                sqlBuilder.OrderBy(filter.SqlOrderByExpression);
            }

            if (!filter.IncludeMetadata) return sqlTemplate.RawSql;

            string totalCountQuery = SqlSelectClauseRegex.Replace(sqlQuery, m => $"{m.Groups[1]} {SqlTotalCountSelectClauseValue} {m.Groups[3]}", 1);

            return $"{sqlTemplate.RawSql}{Environment.NewLine};{totalCountQuery}";
        }
    }
}
