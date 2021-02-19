using System.Collections.Generic;
using Business.Entities;

namespace Business.LogicObjects.Products
{
    public partial interface IProductBlo
    {
        /// <summary>
        /// Get the listings Products
        /// </summary>
        /// <returns>The list of Products </returns>
        IEnumerable<Product> GetProductListing();
    }
}
