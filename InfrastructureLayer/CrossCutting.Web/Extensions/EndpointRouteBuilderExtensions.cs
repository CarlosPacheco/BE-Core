using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrossCutting.Web.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapSignalRRoutes(this IEndpointRouteBuilder hubRouteBuilder, Assembly assembly, string routePrefix = "/")
        {
            //Get SignalR Hubs
            IEnumerable<Type> pluginHubTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Hub)) && !t.IsAbstract);

            MethodInfo mapHubMethod = typeof(HubEndpointRouteBuilderExtensions).GetMethod("MapHub", new[] { typeof(IEndpointRouteBuilder), typeof(string) });
            foreach (Type pluginHubType in pluginHubTypes)
            {
                // Create a generic version of MapHub using the plugin type, then invoke it using hubRouteBuilder as this and the assembly name as the parameter.
                mapHubMethod.MakeGenericMethod(pluginHubType).Invoke(hubRouteBuilder, new[] { (object)hubRouteBuilder, $"{routePrefix}{pluginHubType.Name}" });
            }

            return hubRouteBuilder;
        }

        public static IEndpointRouteBuilder MapSignalRRoutes(this IEndpointRouteBuilder hubRouteBuilder, Assembly assembly, Action<HttpConnectionDispatcherOptions> configureOptions, string routePrefix = "/")
        {
            //Get SignalR Hubs
            IEnumerable<Type> pluginHubTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Hub)) && !t.IsAbstract);

            MethodInfo  mapHubMethod = typeof(HubEndpointRouteBuilderExtensions).GetMethod("MapHub", new[] { typeof(IEndpointRouteBuilder), typeof(string), typeof(Action<HttpConnectionDispatcherOptions>) });
            foreach (Type pluginHubType in pluginHubTypes)
            {
                // Create a generic version of MapHub using the plugin type, then invoke it using hubRouteBuilder as this and the assembly name as the parameter.
                mapHubMethod.MakeGenericMethod(pluginHubType).Invoke(hubRouteBuilder, new[] { (object)hubRouteBuilder, $"{routePrefix}{pluginHubType.Name}", configureOptions});
            }

            return hubRouteBuilder;
        }

        public static IEndpointRouteBuilder MapSignalRRoutes(this IEndpointRouteBuilder hubRouteBuilder, Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                //Get SignalR Hubs
                MapSignalRRoutes(hubRouteBuilder, assembly);
            }
            return hubRouteBuilder;
        }

    }
}
