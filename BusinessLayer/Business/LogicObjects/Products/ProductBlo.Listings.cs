using System.Collections.Generic;
using Business.Entities;

namespace Business.LogicObjects.Products
{
    public partial class ProductBlo
    {
        /// <summary>
        /// Get the listings Products 
        /// </summary>
        /// <returns>The list of Products </returns>
        public IEnumerable<Product> GetProductListing()
        {
            return DataAccess.GetProductListing();
        }

    }
}
