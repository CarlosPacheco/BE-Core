using System.Collections.Generic;
using Application.Services.Listings;
using Business.SearchFilters;
using CrossCutting.Web.Controllers;
using Data.TransferObjects.Listings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DDDReflection.Controllers.Listings
{
    /// <summary>
    /// Listings controller class
    /// </summary>
    [Route("api/[controller]")]
    public partial class ListingsController : BaseApiController, IListingsController
    {
        /// <summary>
        /// Service object layer
        /// </summary>
        private readonly IListingsService _service;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logger"></param>
        public ListingsController(ILogger logger, IListingsService service) : base(logger)
        {
            _service = service;
        }

        // <summary>
        /// Get the listings Products 
        /// </summary>
        /// <returns>The list of Products </returns>
        [Route("caseDetails", Name = "Listings_Product")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ListingDto>), StatusCodes.Status200OK)]
        public IActionResult GetProductListing()
        
        {
            IEnumerable<ListingDto> result =_service.GetProductListing();

            return Ok(result);;
        }

    }
}
