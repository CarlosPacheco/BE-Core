using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Business.Core;
using Business.LogicObjects.MultimediaFiles;
using Business.SearchFilters;
using CrossCutting.Exceptions;
using CrossCutting.Helpers.Helpers;
using CrossCutting.Security.Identity;
using CrossCutting.Web.Mime;
using Data.AccessObjects.Product;

namespace Business.LogicObjects.Product
{
    public partial class ProductBlo : BaseBlo<IProductDao>, IProductBlo
    {
        public IMultimediaBLO MultimediaBLO { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="ProductBlo"/> (Business Controller)
        /// </summary>
        /// <param name="authorization">Security information access object to be used by this instance</param>
        /// <param name="dataAccess">Application Request's data access object to be used by this instance</param>
        public ProductBlo(IProductDao dataAccess, IAuthorization authorization, IMultimediaBLO multimediaBLO) : base(dataAccess, authorization)
        {
            MultimediaBLO = multimediaBLO;
        }

        /// <summary>
        /// Gets a list of Products
        /// </summary>
        /// <param name="searchFilter">Filtering and ordering restrictions</param>
        /// <returns>A list of Products</returns>
        public IEnumerable<Entities.Product.Product> Get(ProductSearchFilter searchFilter)
        {
            return DataAccess.Get(searchFilter);
        }

        /// <summary>
        /// Updates a Product with the specified information
        /// </summary>
        /// <param name="productDto">Patch object containing the new Product value</param>
        /// <returns>The modified Product object</returns>
        public void Update(Entities.Product.Product productDto)
        {
            DataAccess.Update(productDto);
        }

        /// <summary>
        /// Creates a new Product 
        /// </summary>
        /// <param name="productDto">The new entity description object</param>
        /// <returns>The newly created Product</returns>  
        public Entities.Product.Product Create(Entities.Product.Product productDto)
        {
            return GetById(DataAccess.Create(productDto));
        }

        /// <summary>
        /// Gets an Product by it's unique identifier
        /// </summary>
        /// <param name="id">The Product unique identifier</param>
        /// <returns>Product with the specified unique identifier</returns>
        public Entities.Product.Product GetById(int id)
        {
            return DataAccess.GetById(id);
        }


        /// <summary>
        /// Export Products 
        /// </summary>
        /// <param name="searchFilter"></param>
        /// <param name="mediaTypeName"></param>
        /// <returns>The csv file with a list of <see cref="Product"/> instances</returns>    
        public Stream Export(ProductSearchFilter searchFilter, string mediaTypeName)
        {
            IList<dynamic> fileData = DataAccess.Get(searchFilter)
              .Select((Product) => new
              {
                  Product.Id,
                  Product.Name,
                  LastUpdatedOn = Product.UpdatedOn.ToString("dd/MM/yyyy hh:mm")
              }).ToList<dynamic>();

            if (mediaTypeName == MediaType.Application.Csv)
            {
                return FileCreateHelper.Csv(fileData);
            }

            if (mediaTypeName != MediaType.Application.Excel)
            {
                throw new BadRequestException();
            }

            return FileCreateHelper.Excel(fileData);
        }
    }
}
