using CrossCutting.SearchFilters.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossCutting.Web.ActionResults
{
    /// <summary>
    /// Represents an action result that returns a paged data set result (<see cref="PagedDataResponse{T}"/>) with an <see cref="StatusCodes.Status200OK"/>
    /// and paging information on custom "X-Page" headers.
    /// </summary>
    /// <typeparam name="T">The type of content in the entity body.</typeparam>
    public class PagedDataResponse<T> : ActionResult, IStatusCodeActionResult
    {
        /// <summary>
        /// Gets or sets the value to be formatted.
        /// </summary>
        public IPaginatedList<T> Value { get; set; }

        public int? StatusCode => StatusCodes.Status200OK;

        public PagedDataResponse(IEnumerable<T> value)
        {
            Value = (IPaginatedList<T>)value;
        }

        /// <inheritdoc />
        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = StatusCode.Value;

            response.Headers.Add("X-Page-Current", Value.PageCurrent.ToString());
            response.Headers.Add("X-Page-Size", Value.PageSize.ToString());

            if (Value.TotalCount.HasValue)
            {
                response.Headers.Add("X-Page-Total", Value.TotalCount.ToString());
            }

            IActionResultExecutor<JsonResult> executor = context.HttpContext.RequestServices.GetRequiredService<IActionResultExecutor<JsonResult>>();

            return executor.ExecuteAsync(context, new JsonResult(Value));
        }
    }
}