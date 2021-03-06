﻿namespace CrossCutting.SearchFilters.DataAccess.Npgsql
{
    /// <summary>
    /// SQL Query build helper.
    /// </summary>
    public partial class PagedQueryBuilder
    {
        /// <summary>
        /// Template for paged dataset queries using a CTE strategy
        /// </summary>
        private const string CtePagingAndTotalCountQueryTemplate = @";
            WITH ResultQ
            AS (
                {0}
            ), 
            CountQ AS ( 
                SELECT COUNT(1) AS TotalRows FROM ResultQ 
            )
            SELECT 
                ResultQ.*, CountQ.TotalRows
            FROM 
                ResultQ CROSS APPLY CountQ
            ORDER BY {1}
            OFFSET {2} LIMIT {3} ;";

        /// <summary>
        /// Template for paged dataset queries using a OFFSET-FETCH clauses and a placeholder
        /// for ORDER BY clause insertion by <see cref="Dapper.SqlBuilder"/>
        /// </summary>
        private const string PagingQueryTemplate = @"
            {0} 
            /**orderby**/ 
            OFFSET {1} LIMIT {2} ;";
    }
}
