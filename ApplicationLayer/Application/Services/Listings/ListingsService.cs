using System.Collections.Generic;
using AutoMapper;
using Business.LogicObjects.Product;
using Business.SearchFilters;
using CrossCutting.Exceptions;
using CrossCutting.Security.Identity;
using CrossCutting.Web.Services;
using Data.TransferObjects;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Listings
{
    /// <summary>
    /// Listings service class
    /// </summary>
    public partial class ListingsService : BaseService<IProductBlo>, IListingsService
    {
        public ListingsService(IProductBlo businessLogic, IHttpContextAccessor httpContextAccessor, IMapper mapper, IAuthorization authorization) : base(businessLogic, httpContextAccessor, mapper, authorization)
        {       
        }

        /// <summary>
        /// Get the listings Products
        /// </summary>
        /// <returns>The list of Products </returns>
        public IEnumerable<ListingDto> GetProductListing()
        {
            IEnumerable<Business.Entities.Product> result = BusinessLogic.GetProductListing();

            if (result == null)
            {
                throw new EntityNotFoundException();
            }

            return Mapper.Map<IEnumerable<ListingDto>>(result);
        }
    }
}
