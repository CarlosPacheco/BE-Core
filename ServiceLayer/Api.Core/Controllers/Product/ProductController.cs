using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using Application.Services.Product;
using Business.SearchFilters;
using CrossCutting.Web.Controllers;
using CrossCutting.Web.Mime;
using Data.TransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Api.Core.Controllers.Product
{
    [Route("api/[controller]")]
    public class ProductController : BaseApiController, IProductController
    {
        /// <summary>
        /// Service object layer
        /// </summary>
        private readonly IProductService _service;

        public ProductController(ILogger logger, IProductService service) : base(logger)
        {
            _service = service;
        }

        /// <summary>
        /// Gets a list of Products
        /// </summary>
        /// <param name="searchFilter">Filtering and ordering restrictions</param>
        /// <returns>A list of Products</returns>
        [HttpGet, ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get([FromQuery] ProductSearchFilter searchFilter)
        {
            return PagedData(_service.Get(searchFilter));
        }

        /// <summary>
        /// Updates a Product with the specified information
        /// </summary>
        /// <param name="id">Product unique identifier</param>
        /// <param name="productDto">Patch object containing the new Product value</param>
        /// <returns>The modified Product object</returns>
        [Route("{id}")]
        [HttpPatch, ProducesResponseType(typeof(void), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int id, ProductDto productDto, [FromForm] IFormFile file)
        {
            if (productDto != null && id != productDto.Id)
            {
                return BadRequest();
            }

            _service.Update(productDto, file);

            return Ok();
        }

        /// <summary>
        /// Creates a new Product 
        /// </summary>
        /// <param name="productDto">The new entity description object</param>
        /// <returns>The newly created Product</returns>       
        [Route("", Name = "Product_Create")]
        [HttpPost, ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public IActionResult Create(ProductDto productDto, [FromForm] IFormFile file)
        {
            ProductDto newproductDto = _service.Create(productDto, file);

            return CreatedAtRoute("Product_GetById", new { id = productDto.Id }, newproductDto);
        }

        /// <summary>
        /// Gets an Product by it's unique identifier
        /// </summary>
        /// <param name="id">The Product unique identifier</param>
        /// <returns>Product with the specified unique identifier</returns>
        [Route("{id}", Name = "Product_GetById")]
        [HttpGet, ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            ProductDto productDto = _service.GetById(id);

            return productDto == null ? NotFound() : Ok(productDto);
        }

        /// <summary>
        /// Export Product Details 
        /// </summary>
        /// <param name="searchFilter"></param>
        /// <param name="mediaTypeName"></param>
        /// <returns>The csv file with a list of <see cref="CaseDetails"/> instances</returns>  
        [Route("export", Name = "Product_Export")]
        [HttpGet, ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public IActionResult Export([FromQuery] ProductSearchFilter searchFilter, [FromHeader(Name = "Content-Type")] string mediaTypeName)
        {
            Stream stream = _service.Export(searchFilter, mediaTypeName);

            return File(stream, MediaTypeNames.Application.Octet, $"ProductList{DateTime.UtcNow:o}{(mediaTypeName == MediaType.Application.Excel ? ".xlsx" : ".csv")}");
        }
    }
}