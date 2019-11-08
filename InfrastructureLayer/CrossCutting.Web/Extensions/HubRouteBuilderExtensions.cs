using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CrossCutting.Web.Extensions
{
    public static class HubRouteBuilderExtensions
    {
        // The MethodInfo for MapHub, this can be stored statically because it's just a pointer to the function.
        private static readonly MethodInfo MapHubMethod = typeof(HubRouteBuilder).GetMethod("MapHub", new[] { typeof(string) });

        public static HubRouteBuilder MapSignalRRoutes(this HubRouteBuilder hubRouteBuilder, Assembly assembly)
        {
            //Get SignalR Hubs
            IEnumerable<Type> pluginHubTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Hub)) && !t.IsAbstract);

            foreach (Type pluginHubType in pluginHubTypes)
            {
                // Create a generic version of MapHub using the plugin type, then invoke it using hubRouteBuilder as this and the assembly name as the parameter.
                MapHubMethod.MakeGenericMethod(pluginHubType).Invoke(hubRouteBuilder, new object[] { "/" + pluginHubType.Name });
            }

            return hubRouteBuilder;
        }

        public static HubRouteBuilder MapSignalRRoutes(this HubRouteBuilder hubRouteBuilder, Assembly[] assemblies)
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
