using Business.Entities;
using System.Collections.Generic;

namespace Interfaces.Data.AccessObjects.Products
{
    public partial interface IProductDao
    {
        /// <summary>
        /// Get the listings Products
        /// </summary>
        /// <returns>The list of Products </returns>
        IEnumerable<Product> GetProductListing();
    }
}
