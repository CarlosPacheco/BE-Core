using System.Collections.Generic;
using Business.SearchFilters;
using Data.TransferObjects.Listings;

namespace Application.Services.Listings
{
    /// <summary>
    /// Listings controller implementation definition
    /// </summary>
    public partial interface IListingsService
    {
        /// <summary>
        /// Get the listings Products 
        /// </summary>
        /// <returns>The list of Products </returns>
        IEnumerable<ListingDto> GetProductListing();
    }
}
