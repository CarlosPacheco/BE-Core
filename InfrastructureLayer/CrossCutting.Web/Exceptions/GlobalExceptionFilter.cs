using CrossCutting.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;
using System.Net.Mime;

namespace CrossCutting.Web.Exceptions
{
    ///Prefer middleware for exception handling. 
    ///Use exception filters only where error handling differs based on which action method is called. 
    ///For example, an app might have action methods for both API endpoints and for views/HTML. 
    ///The API endpoints could return error information as JSON, while the view-based actions could return an error page as HTML.
    ///https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-2.2#exception-filters
    /// <summary>
    /// Global Exception filter to control Business and Data layers thrown exceptions.
    /// </summary>
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        protected ILogger Logger { get; }

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public GlobalExceptionFilter(ILogger logger, IWebHostEnvironment hostingEnvironment,
        IModelMetadataProvider modelMetadataProvider)
        {
            Logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        /// <summary>
        /// Raises an Exception event.
        /// </summary>
        /// <param name="executedContext">The current Http action context object.</param>
        public override void OnException(ExceptionContext executedContext)
        {
            JsonResult responseMessage = new JsonResult(executedContext.Exception.Message);
            responseMessage.ContentType = MediaTypeNames.Application.Json;

            if (executedContext.Exception is ICustomException)
            {
                if (executedContext.Exception is EntityNotFoundException)
                {
                    responseMessage.StatusCode = StatusCodes.Status404NotFound;
                }

                if (executedContext.Exception is DataAccessException)
                {
                    responseMessage.StatusCode = StatusCodes.Status500InternalServerError;
                }
            }
            else
            {
                responseMessage.Value ="Oops! Looks like something went wrong... :(";
                responseMessage.StatusCode = StatusCodes.Status500InternalServerError;
            }

            // Handle web api exception
            //executedContext.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //executedContext.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            executedContext.Result = responseMessage;

            Logger.Error(executedContext.Exception.Message, executedContext.Exception);

            //To handle an exception, set the ExceptionHandled property to true or write a response.
            //This stops propagation of the exception. An exception filter can't turn an exception into a "success". 
            //Only an action filter can do that.
            //executedContext.ExceptionHandled = true;

            base.OnException(executedContext);
        }


    }
}