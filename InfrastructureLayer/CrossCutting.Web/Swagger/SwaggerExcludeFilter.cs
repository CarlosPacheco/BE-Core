using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CrossCutting.Web.Swagger
{
    /// <summary>
    /// Exclusion filter for swagger definition
    /// </summary>
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null || context == null || context.Type == null)
            {
                return;
            }

            IEnumerable<PropertyInfo> toExcludeProperties = context.Type.GetProperties().Where(t => t.GetCustomAttribute<SwaggerExcludedAttribute>() != null);

            foreach (PropertyInfo toExcludedProperty in toExcludeProperties)
            {
                if (schema.Properties.ContainsKey(toExcludedProperty.Name))
                {
                    schema.Properties.Remove(toExcludedProperty.Name);
                }
            }
        }
    }
}
