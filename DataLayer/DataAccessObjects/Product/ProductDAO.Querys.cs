using Business.SearchFilters;
using Dapper;

namespace Data.AccessObjects.Product
{
    public partial class ProductDao
    {
        private const string GetQuery = @"/* Product GetQuery */
            SELECT *
            FROM Product CD
            INNER JOIN LOH L ON CD.IDLOH = L.ID
            INNER JOIN ProductGroup CDG ON CDG.Id = CD.IdProductGroup
            INNER JOIN 
            ( 
              SELECT Id, FilePath Name
              FROM Product
            ) M ON M.Id = CD.Id
            /**where**/
            ";

        /// <summary>
        /// Sql query template for GetByIdentifier method.
        /// Dapper SqlBuilder placeholders included in.
        /// </summary>
        private const string QueryGetByIdentifier = @"/* Product SqlGetByIdentifierQuery */
            SELECT CD.*, L.*, CDG.*, M.*
            FROM Product CD 
            INNER JOIN LOH L ON CD.IDLOH = L.ID
            INNER JOIN ProductGroup CDG ON CDG.Id = CD.IdProductGroup
            INNER JOIN 
            ( 
              SELECT Id, FilePath Name
              FROM Product
            ) M ON M.Id = CD.Id
            WHERE CD.Id = @Id ";
      

        private string GetGetQuery(ProductSearchFilter filter, out object parameters)
        {
            SqlBuilder sqlBuilder = new SqlBuilder();
            SqlBuilder.Template sqlTemplate = sqlBuilder.AddTemplate(GetQuery);           

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                sqlBuilder.Where("CDG.Name LIKE :Name", new { Name = $"%{filter.Name}%" });
            }

            if (filter.UpdatedOnStart.HasValue)
            {
                sqlBuilder.Where("CD.UpdatedOn >= TO_TIMESTAMP(:UpdatedOnStart, 'YYYY-MM-DD HH24:MI:SS.FF')", new { UpdatedOnStart = $"{filter.UpdatedOnStart.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")}" });
            }

            if (filter.UpdatedOnEnd.HasValue)
            {
                sqlBuilder.Where("CD.UpdatedOn <= TO_TIMESTAMP(:UpdatedOnEnd, 'YYYY-MM-DD HH24:MI:SS.FF')", new { UpdatedOnEnd = $"{filter.UpdatedOnEnd.Value.ToString("yyyy-MM-dd HH:mm:ss.fff")}"});
            }

            // Return all parameters to be reused (passed to Dapper exec/query method)
            parameters = sqlTemplate.Parameters;

            return sqlTemplate.RawSql;
        }
    }
}
