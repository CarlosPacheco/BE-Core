
namespace CrossCutting.SearchFilters.DataAccess
{
    public interface IPagedQueryBuilder
    {
        /// <summary>
        /// Builds a query wrapper to retrieve a paged result set.
        /// </summary>
        /// <param name="sqlQuery">The get sql query to be paged</param>
        /// <param name="orderByClause">The full ORDER BY sql clause</param>
        /// <param name="offset">The records to start from when retrieving the result set</param>
        /// <param name="numberOfRows">The number of row to retrive in the data set</param>
        /// <returns>The specified number of rows, oredered as the order clause, starting from the offset value</returns>
        string PagedSqlQuery(string sqlQuery, string orderByClause, int offset, int numberOfRows);

        /// <summary>
        /// Builds a query wrapper to retrieve a paged result set and it's total row count
        /// </summary>
        /// <param name="sqlQuery">The get sql query to be paged</param>
        /// <param name="orderByClauseMembers">The sql ORDER BY clause value. <example>Id DESC, Name ASC</example></param>
        /// <param name="rowOffset">The rows to skip when retrieving the result set</param>
        /// <param name="pageSize">The number of rows to retrive in the data set</param>
        /// <returns>The specified page rows, ordered</returns>
        string PagedQueryWithTotals(string sqlQuery, int rowOffset, int pageSize, string orderByClauseMembers);

        /// <summary>
        /// Builds a query wrapper to retrieve a paged result set.
        /// </summary>
        /// <param name="sqlQuery">The get sql query to be paged</param>
        /// <param name="filter">The Filter object</param>
        /// <returns>The specified number of rows, oredered as the order clause, starting from the offset value</returns>
        string PagedQuery(string sqlQuery, ISearchFilter filter, ref object param);
    }
}
