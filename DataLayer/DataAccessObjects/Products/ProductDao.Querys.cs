using Business.SearchFilters;
using Dapper;

namespace Data.AccessObjects.Products
{
    public partial class ProductDao
    {
        private const string GetQuery = @"/* Product GetQuery */
            SELECT *
            FROM Product CD
            INNER JOIN Item L ON CD.IDItem = L.ID
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
            INNER JOIN Item L ON CD.IDItem = L.ID
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
                sqlBuilder.Where("CDG.Name LIKE @Name", new { Name = $"%{filter.Name}%" });
            }

            if (filter.UpdatedOnStart.HasValue)
            {
                sqlBuilder.Where("CD.UpdatedOn >= @UpdatedOnStart", new { filter.UpdatedOnStart });
            }

            if (filter.UpdatedOnEnd.HasValue)
            {
                sqlBuilder.Where("CD.UpdatedOn <= @UpdatedOnEnd", new { filter.UpdatedOnEnd });
            }

            // Return all parameters to be reused (passed to Dapper exec/query method)
            parameters = sqlTemplate.Parameters;

            return sqlTemplate.RawSql;
        }
    }
}
