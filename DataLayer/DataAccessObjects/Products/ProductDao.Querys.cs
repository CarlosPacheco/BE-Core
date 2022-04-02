using Business.SearchFilters;
using Dapper;

namespace Data.AccessObjects.Products
{
    public partial class ProductDao
    {
        private const string GetQuery = @"/* Product GetQuery */
            SELECT *
            FROM Product P
            INNER JOIN Item I ON P.IDItem = I.ID
            INNER JOIN ProductGroup PG ON PG.Id = P.IdProductGroup
            INNER JOIN 
            ( 
              SELECT Id, FilePath Name
              FROM Product
            ) M ON M.Id = P.Id
            /**where**/
            ";

        /// <summary>
        /// Sql query template for GetByIdentifier method.
        /// Dapper SqlBuilder placeholders included in.
        /// </summary>
        private const string QueryGetByIdentifier = @"/* Product SqlGetByIdentifierQuery */
            SELECT P.*, I.*, PG.*, M.*
            FROM Product P 
            INNER JOIN Item I ON P.IDItem = I.ID
            INNER JOIN ProductGroup PG ON PG.Id = P.IdProductGroup
            INNER JOIN 
            ( 
              SELECT Id, FilePath Name
              FROM Product
            ) M ON M.Id = P.Id
            WHERE P.Id = @Id ";

        private string GetGetQuery(ProductSearchFilter filter, out object parameters)
        {
            SqlBuilder sqlBuilder = new SqlBuilder();
            SqlBuilder.Template sqlTemplate = sqlBuilder.AddTemplate(GetQuery);

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                sqlBuilder.Where("PG.Name LIKE @Name", new { Name = $"%{filter.Name}%" });
            }

            if (filter.UpdatedOnStart.HasValue)
            {
                sqlBuilder.Where("P.UpdatedOn >= @UpdatedOnStart", new { filter.UpdatedOnStart });
            }

            if (filter.UpdatedOnEnd.HasValue)
            {
                sqlBuilder.Where("P.UpdatedOn <= @UpdatedOnEnd", new { filter.UpdatedOnEnd });
            }

            // Return all parameters to be reused (passed to Dapper exec/query method)
            parameters = sqlTemplate.Parameters;

            return sqlTemplate.RawSql;
        }
    }
}
