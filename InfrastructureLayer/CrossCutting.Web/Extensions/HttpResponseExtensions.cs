using CrossCutting.Exceptions.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CrossCutting.Web.Extensions
{
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Write a JSON Response on the HttpResponse
        /// </summary>
        /// <param name="statusCode">Use Microsoft.AspNetCore.Http.StatusCodes</param>
        /// <param name="exception"></param>
        public static Task WriteJsonResponseAsync(this HttpResponse httpResponse, int statusCode, Exception exception = null)
        {
            httpResponse.ContentType = MediaTypeNames.Application.Json;
            httpResponse.StatusCode = statusCode;
            return httpResponse.WriteAsync(JsonConvert.SerializeObject(new ExceptionInfo(statusCode, exception)));
        }

        /// <summary>
        /// Write a JSON Response on the HttpResponse
        /// </summary>
        /// <param name="statusCode">Use Microsoft.AspNetCore.Http.StatusCodes</param>
        /// <param name="message"></param>
        public static Task WriteJsonResponseAsync(this HttpResponse httpResponse, int statusCode, string message)
        {
            httpResponse.ContentType = MediaTypeNames.Application.Json;
            httpResponse.StatusCode = statusCode;
            return httpResponse.WriteAsync(JsonConvert.SerializeObject(new ExceptionInfo(statusCode, message)));
        }
    }
}
