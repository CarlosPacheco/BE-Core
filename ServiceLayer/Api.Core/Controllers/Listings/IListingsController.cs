using Data.TransferObjects.Listings;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DDDReflection.Controllers.Listings
{
    /// <summary>
    /// Listings controller implementation definition
    /// </summary>
    public partial interface IListingsController
    {
        /// <summary>
        /// Get the listings Products 
        /// </summary>
        /// <returns>The list of Products </returns>
        IActionResult GetProductListing();
    }
}
