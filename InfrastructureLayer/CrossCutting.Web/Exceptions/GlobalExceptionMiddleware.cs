using System;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using CrossCutting.Exceptions;
using CrossCutting.Exceptions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace CrossCutting.Web.Exceptions
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private const string DefaultErrorMessage = "This is not looking good - for either of us!";

        /// <summary>
        /// A public constructor with a parameter of type RequestDelegate.
        /// </summary>
        /// <param name="next"></param>
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// A public method named Invoke or InvokeAsync. This method must:
        /// Return a Task.
        /// Accept a first parameter of type HttpContext.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="logger">Additional parameters for the constructor and Invoke/InvokeAsync are populated by dependency injection (DI).</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext, ILogger logger, IOptions<JsonOptions> options)
        {
            try
            {
               // IExceptionHandlerFeature exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerFeature>();

                await _next(httpContext);
            }
            catch (Exception exception)
            {
                logger.Error($"[API-Error] => {exception}");
                await HandleAsync(httpContext, exception, options);
            }
        }

        /// <summary>When overridden in a derived class, handles the exception asynchronously.</summary>
        /// <returns>A task representing the asynchronous exception handling operation.</returns>
        /// <param name="httpContext">The exception handler context.</param>
        // TODO: Refactor Switch statements, minimize repeated code.
        public static Task HandleAsync(HttpContext httpContext, Exception exception, IOptions<JsonOptions> options)
        {
            // Access Exception
            ExceptionInfo responseMessage = null;
            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            
            switch (exception)
            {
                case ICustomException customException:
                    switch (customException)
                    {
                        case EntityNotFoundException _:
                            responseMessage = new ExceptionInfo(exception);
                            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                            break;

                        case DataAccessException _:
                        case IdentityConfigurationException _:
                            // Define message for this type of exception if suited. Default 500 status code seems a fit.
                            responseMessage = new ExceptionInfo(exception);
                            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                            break;

                        case BadRequestException _:
                            responseMessage = new ExceptionInfo(exception);
                            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                            break;

                        case ForbiddenAccessException _:
                            responseMessage = new ExceptionInfo(exception);
                            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

                            break;

                        case MimeMultipartException _:
                            responseMessage = new ExceptionInfo(exception);

                            httpContext.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;

                            break;

                        case BadGatewayException _:
                            responseMessage = new ExceptionInfo(exception);
                            httpContext.Response.StatusCode = StatusCodes.Status502BadGateway;

                            break;
                    }
                    break;

                default:
                    
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    responseMessage = new ExceptionInfo(DefaultErrorMessage, exception);

                    break;
            }

            return httpContext.Response.WriteAsync(JsonSerializer.Serialize(responseMessage, options.Value.JsonSerializerOptions));
        }

    }

}