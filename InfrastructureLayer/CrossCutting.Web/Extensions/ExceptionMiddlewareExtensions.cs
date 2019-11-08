using Serilog;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Builder;
using CrossCutting.Web.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace CrossCutting.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app, ILogger logger, IOptions<JsonOptions> options)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    // Use exceptionHandlerPathFeature to process the exception (for example, 
                    // logging), but do NOT expose sensitive error information directly to 
                    // the client.
                    IExceptionHandlerFeature exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();
                    logger.Error($"[API-Error] => {exceptionHandlerPathFeature.Error}");

                    await GlobalExceptionMiddleware.HandleAsync(context, exceptionHandlerPathFeature.Error, options); 
                });
            });
        }

        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
