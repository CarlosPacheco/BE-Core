using System.Collections.Generic;
using Business.SearchFilters;
using Data.Core.Interfaces;

namespace Data.AccessObjects.Product
{
    public partial interface IProductDao : IBaseDao
    {
        /// <summary>
        /// Gets a list of Products
        /// </summary>
        /// <param name="searchFilter">Filtering and ordering restrictions</param>
        /// <returns>A list of Products</returns>
        IEnumerable<Business.Entities.Product> Get(ProductSearchFilter searchFilter);

        /// <summary>
        /// Updates a Product with the specified information
        /// </summary>
        /// <param name="productDto">Patch object containing the new Product value</param>
        /// <returns>The modified Product object</returns>
        void Update(Business.Entities.Product productDto);

        /// <summary>
        /// Creates a new Product 
        /// </summary>
        /// <param name="productDto">The new entity description object</param>
        /// <returns>The newly created Product</returns>  
        int Create(Business.Entities.Product productDto);

        /// <summary>
        /// Gets an Product by it's unique identifier
        /// </summary>
        /// <param name="id">The Product unique identifier</param>
        /// <returns>Product with the specified unique identifier</returns>
        Business.Entities.Product GetById(int id);
    }
}
