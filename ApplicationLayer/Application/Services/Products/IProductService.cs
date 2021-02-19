using System.Collections.Generic;
using System.IO;
using Business.Core.Services;
using Business.LogicObjects.Products;
using Business.SearchFilters;
using Data.TransferObjects;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Products
{
    public interface IProductService : IBaseService<IProductBlo>
    {
        /// <summary>
        /// Gets a list of Products
        /// </summary>
        /// <param name="searchFilter">Filtering and ordering restrictions</param>
        /// <returns>A list of Products</returns>
        IEnumerable<ProductDto> Get(ProductSearchFilter searchFilter);

        /// <summary>
        /// Updates a Product with the specified information
        /// </summary>
        /// <param name="productDto">Patch object containing the new Product value</param>
        /// <returns>The modified Product object</returns>
        void Update(ProductDto productDto, IFormFile file);

        /// <summary>
        /// Creates a new Product 
        /// </summary>
        /// <param name="productDto">The new entity description object</param>
        /// <returns>The newly created Product</returns>  
        ProductDto Create(ProductDto productDto, IFormFile file);

        /// <summary>
        /// Gets an Product by it's unique identifier
        /// </summary>
        /// <param name="id">The Product unique identifier</param>
        /// <returns>Product with the specified unique identifier</returns>
        ProductDto GetById(int id);

        /// <summary>
        /// Export Product
        /// </summary>
        /// <param name="searchFilter"></param>
        /// <param name="mediaTypeName"></param>
        /// <returns>The csv file with a list of <see cref="Product"/> instances</returns>    
        Stream Export(ProductSearchFilter searchFilter, string mediaTypeName);
    }
}
