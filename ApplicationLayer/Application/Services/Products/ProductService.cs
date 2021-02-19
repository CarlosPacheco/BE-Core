using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Business.Core.Services;
using Business.Entities;
using Business.LogicObjects.Products;
using Business.SearchFilters;
using CrossCutting.SearchFilters.Extensions;
using CrossCutting.Security.Identity;
using Data.TransferObjects;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Products
{
    public class ProductService : BaseService<IProductBlo>, IProductService
    {
        public ProductService(IProductBlo businessLogic, IHttpContextAccessor httpContextAccessor, IMapper mapper, IAuthorization authorization) : base(businessLogic, httpContextAccessor, mapper, authorization)
        {
        }

        /// <summary>
        /// Gets a list of Products
        /// </summary>
        /// <param name="searchFilter">Filtering and ordering restrictions</param>
        /// <returns>A list of Products</returns>
        public IEnumerable<ProductDto> Get(ProductSearchFilter searchFilter)
        {
            IEnumerable<Product> lotList = BusinessLogic.Get(searchFilter);
            IEnumerable<ProductDto> lotDto = Mapper.MapPaged<ProductDto, Product>(lotList);

            return lotDto;
        }

        /// <summary>
        /// Updates a Product with the specified information
        /// </summary>
        /// <param name="caseDto">Patch object containing the new Product value</param>
        /// <returns>The modified Product object</returns>
        public void Update(ProductDto caseDto, IFormFile file)
        {
            Product Product = Mapper.Map<Product>(caseDto);

            BusinessLogic.Update(Product);
        }

        /// <summary>
        /// Creates a new Product 
        /// </summary>
        /// <param name="caseDto">The new entity description object</param>
        /// <returns>The newly created Product</returns>  
        public ProductDto Create(ProductDto productDto, IFormFile file)
        {
            Product product = BusinessLogic.Create(Mapper.Map<Product>(productDto));

            return GetById(product.Id.Value);
        }

        /// <summary>
        /// Gets an Product by it's unique identifier
        /// </summary>
        /// <param name="id">The Product unique identifier</param>
        /// <returns>Product with the specified unique identifier</returns>
        public ProductDto GetById(int id)
        {
            Product product = BusinessLogic.GetById(id);
            ProductDto productDto = Mapper.Map<ProductDto>(product);

            return productDto;
        }

        /// <summary>
        /// Export Product
        /// </summary>
        /// <param name="searchFilter"></param>
        /// <param name="mediaTypeName"></param>
        /// <returns>The csv file with a list of <see cref="Product"/> instances</returns>    
        public Stream Export(ProductSearchFilter searchFilter, string mediaTypeName)
        {
            return BusinessLogic.Export(searchFilter, mediaTypeName);
        }
    }
}
