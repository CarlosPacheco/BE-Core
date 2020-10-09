using System.Collections.Generic;
using Business.SearchFilters;

namespace Business.LogicObjects.Product
{
    public partial interface IProductBlo
    {
        /// <summary>
        /// Get the listings Products
        /// </summary>
        /// <returns>The list of Products </returns>
        IEnumerable<Entities.Product> GetProductListing();
    }
}
