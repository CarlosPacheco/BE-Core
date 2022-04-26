using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using Application.Services.Products;
using Business.SearchFilters;
using CrossCutting.Web.Controllers;
using CrossCutting.Web.Mime;
using Data.TransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Core.Controllers.Products
{
    [Route("api/[controller]")]
    public class ProductController : BaseApiController, IProductController
    {
        /// <summary>
        /// Service object layer
        /// </summary>
        private readonly IProductService _service;

        public ProductController(ILogger<ProductController> logger, IProductService service) : base(logger)
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
        /// <param name="dto">Patch object containing the new Product value</param>
        /// <returns>The modified Product object</returns>
        [Route("")]
        [HttpPut, ProducesResponseType(typeof(void), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(ProductDto dto, [FromForm] IFormFile file)
        {
            if (dto == null || dto.Id == null || dto.Id <= 0)
            {
                return BadRequest();
            }

            _service.Update(dto, file);

            return Ok();
        }

        /// <summary>
        /// Creates a new Product 
        /// </summary>
        /// <param name="dto">The new entity description object</param>
        /// <returns>The newly created Product</returns>       
        [Route("", Name = "Product_Create")]
        [HttpPost, ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public IActionResult Create(ProductDto dto, [FromForm] IFormFile file)
        {
            ProductDto newDto = _service.Create(dto, file);

            return CreatedAtRoute("Product_GetById", new { id = newDto.Id }, newDto);
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
            ProductDto? productDto = _service.GetById(id);

            return productDto == null ? NotFound() : Ok(productDto);
        }

        /// <summary>
        /// Export Product Details 
        /// </summary>
        /// <param name="searchFilter"></param>
        /// <param name="mediaTypeName"></param>
        /// <returns>The csv file with a list of <see cref="CaseDetails"/> instances</returns>  
        [Route("export", Name = "Product_Export")]
        [HttpGet, ProducesResponseType(typeof(void), StatusCodes.Status200OK)]//TODO: HeaderNames.ContentType
        public IActionResult Export([FromQuery] ProductSearchFilter searchFilter, [FromHeader(Name = "Content-Type")] string mediaTypeName)
        {
            Stream stream = _service.Export(searchFilter, mediaTypeName);

            return File(stream, MediaTypeNames.Application.Octet, $"ProductList{DateTime.UtcNow:o}{(mediaTypeName == MediaType.Application.Excel ? ".xlsx" : ".csv")}");
        }
    }
}