﻿using System.Collections.Generic;
using System.Net.Mime;
using CrossCutting.Web.ActionResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CrossCutting.Web.Controllers
{
    /// <summary>
    /// Base class for all API controllers
    /// </summary>
    [Produces(MediaTypeNames.Application.Json), ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// Security Principal for authentication and authorization information access
        /// </summary>
        public BaseApiController(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Creates a negotiated content result (200 Ok) for a paged result set.
        /// </summary>
        /// <typeparam name="T">The type of content in the entity body.</typeparam>
        /// <param name="content">The content value to negotiate and format in the entity body.</param>
        /// <returns>A negotiated content result with the specified values.</returns>
        protected PagedDataResponse<T> PagedData<T>(IEnumerable<T> content)
        {
            return new PagedDataResponse<T>(content);
        }

        /// <summary>
        /// Creates a negotiated content result (501 Not Implemented) for NotImplementedResult.
        /// </summary>
        /// <returns>A negotiated content result with the specified values.</returns>
        protected NotImplementedResult NotImplemented()
        {
            return new NotImplementedResult();
        }
    }
}
