using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Business.LogicObjects.Product;
using Business.SearchFilters;
using CrossCutting.SearchFilters.Extensions;
using CrossCutting.Security.Identity;
using CrossCutting.Web.Services;
using Data.TransferObjects.Product;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Product
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
            IEnumerable<Business.Entities.Product.Product> lotList = BusinessLogic.Get(searchFilter);
            IEnumerable<ProductDto> lotDto = Mapper.MapPaged<ProductDto, Business.Entities.Product.Product>(lotList);

            return lotDto;
        }

        /// <summary>
        /// Updates a Product with the specified information
        /// </summary>
        /// <param name="caseDto">Patch object containing the new Product value</param>
        /// <returns>The modified Product object</returns>
        public void Update(ProductDto caseDto, IFormFile file)
        {
            Business.Entities.Product.Product Product = Mapper.Map<Business.Entities.Product.Product>(caseDto);
           
            BusinessLogic.Update(Product);
        }

        /// <summary>
        /// Creates a new Product 
        /// </summary>
        /// <param name="caseDto">The new entity description object</param>
        /// <returns>The newly created Product</returns>  
        public ProductDto Create(ProductDto productDto, IFormFile file)
        {
            Business.Entities.Product.Product product = BusinessLogic.Create(Mapper.Map<Business.Entities.Product.Product>(productDto));

            return GetById(product.Id.Value);
        }

        /// <summary>
        /// Gets an Product by it's unique identifier
        /// </summary>
        /// <param name="id">The Product unique identifier</param>
        /// <returns>Product with the specified unique identifier</returns>
        public ProductDto GetById(int id)
        {
            Business.Entities.Product.Product product = BusinessLogic.GetById(id);
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
