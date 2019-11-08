using System;
using Microsoft.AspNetCore.Builder;

namespace CrossCutting.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use Cors Policy (Custom)
        /// </summary>
        /// <param name="appBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseCors(ServiceCollectionExtensions.CorsPolicyName);
            return appBuilder;
        }
    }
}
