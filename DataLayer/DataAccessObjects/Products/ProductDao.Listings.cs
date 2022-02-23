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
            string query = $@"SELECT DISTINCT P.Id, P.Name
            FROM Product P
            INNER JOIN Item I ON P.IdItem = I.Id";

            return DbConnection.Query<Product>(query).AsList();
        }
    }
}
