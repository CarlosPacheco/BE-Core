using System.Collections.Generic;
using Business.SearchFilters;

namespace Business.LogicObjects.Product
{
    public partial class ProductBlo
    {
        /// <summary>
        /// Get the listings Products 
        /// </summary>
        /// <returns>The list of Products </returns>
        public IEnumerable<Entities.Product> GetProductListing()
        {
            return DataAccess.GetProductListing();
        }

    }
}
