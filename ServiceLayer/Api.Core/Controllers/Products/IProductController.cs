using Business.SearchFilters;
using Data.TransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Core.Controllers.Products
{
    public interface IProductController
    {
        /// <summary>
        /// Gets a list of Products
        /// </summary>
        /// <param name="searchFilter">Filtering and ordering restrictions</param>
        /// <returns>A list of Products</returns>
        IActionResult Get([FromQuery] ProductSearchFilter searchFilter);

        /// <summary>
        /// Updates a Product with the specified information
        /// </summary>
        /// <param name="id">Product unique identifier</param>
        /// <param name="caseDto">Patch object containing the new Product value</param>
        /// <returns>The modified Product object</returns>
        IActionResult Update(int id, ProductDto caseDto, IFormFile file);

        /// <summary>
        /// Creates a new Product 
        /// </summary>
        /// <param name="caseDto">The new entity description object</param>
        /// <returns>The newly created Product</returns>  
        IActionResult Create(ProductDto caseDto, IFormFile file);

        /// <summary>
        /// Gets an Product by it's unique identifier
        /// </summary>
        /// <param name="id">The Product unique identifier</param>
        /// <returns>Product with the specified unique identifier</returns>
        IActionResult GetById(int id);

        /// <summary>
        /// Export Product Details 
        /// </summary>
        /// <param name="searchFilter"></param>
        /// <param name="mediaTypeName"></param>
        /// <returns>The csv file with a list of <see cref="CaseDetails"/> instances</returns>  
        IActionResult Export([FromQuery] ProductSearchFilter searchFilter, [FromHeader(Name = "Content-Type")] string mediaTypeName);
    }
}
