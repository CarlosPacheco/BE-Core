﻿using System.Collections.Generic;
using Business.Core.Data.Interfaces;
using Business.Entities;
using Business.SearchFilters;

namespace Interfaces.Data.AccessObjects.Products
{
    public partial interface IProductDao : IBaseDao
    {
        /// <summary>
        /// Gets a list of Products
        /// </summary>
        /// <param name="searchFilter">Filtering and ordering restrictions</param>
        /// <returns>A list of Products</returns>
        IEnumerable<Product> Get(ProductSearchFilter searchFilter);

        /// <summary>
        /// Updates a Product with the specified information
        /// </summary>
        /// <param name="productDto">Patch object containing the new Product value</param>
        /// <returns>The modified Product object</returns>
        void Update(Product productDto);

        /// <summary>
        /// Creates a new Product 
        /// </summary>
        /// <param name="productDto">The new entity description object</param>
        /// <returns>The newly created Product</returns>  
        int Create(Product productDto);

        /// <summary>
        /// Gets an Product by it's unique identifier
        /// </summary>
        /// <param name="id">The Product unique identifier</param>
        /// <returns>Product with the specified unique identifier</returns>
        Product? GetById(int id);
    }
}
