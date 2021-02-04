using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CrossCutting.Web.ActionResults
{
    /// <summary>
    /// A <see cref="StatusCodeResult"/> that when executed will produce a
    /// <see cref="StatusCodes.Status501NotImplemented"/> response.
    /// </summary>
    public class NotImplementedResult : ActionResult, IStatusCodeActionResult
    {
        public int? StatusCode => StatusCodes.Status501NotImplemented;
    }
}
