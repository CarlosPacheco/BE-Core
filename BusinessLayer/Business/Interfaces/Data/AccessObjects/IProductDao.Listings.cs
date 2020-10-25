using System.Collections.Generic;
using Business.SearchFilters;

namespace Data.AccessObjects
{
    public partial interface IProductDao
    {
        /// <summary>
        /// Get the listings Products
        /// </summary>
        /// <returns>The list of Products </returns>
        IEnumerable<Business.Entities.Product> GetProductListing();
    }
}
