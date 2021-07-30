using System.Collections.Generic;
using Business.Entities;
using Dapper;

namespace Data.AccessObjects.Products
{
    public partial class ProductDao
    {
        /// <summary>
        /// Get the listings Products 
        /// </summary>
        /// <returns>The list of Products </returns>
        public IEnumerable<Product> GetProductListing()
        {
            string query = $@"SELECT DISTINCT L.Id, L.CaseNumber, CD.Id
            FROM Product CD
            INNER JOIN Item L ON CD.IdItem = L.Id";

            return DbConnection.Query<Product>(query).AsList();
        }
    }
}
