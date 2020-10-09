using System.Collections.Generic;
using System.Linq;

using Business.SearchFilters;
using Dapper;

namespace Data.AccessObjects.Product
{
    public partial class ProductDao
    {
        /// <summary>
        /// Get the listings Products 
        /// </summary>
        /// <returns>The list of Products </returns>
        public IEnumerable<Business.Entities.Product> GetProductListing()
        {
            string query = $@"SELECT DISTINCT L.Id, L.CaseNumber, CD.Id
            FROM Product CD
            INNER JOIN Loh L ON CD.IdLoh = L.Id";

            return DbConnection.Query<Business.Entities.Product>(query).ToList();
        }

    }
}
