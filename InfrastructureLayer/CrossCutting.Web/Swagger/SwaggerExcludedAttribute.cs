using System;

namespace CrossCutting.Web.Swagger
{
    /// <summary>
    /// Indicates that a property should be excluded from swagger schema definition
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerExcludedAttribute : Attribute
    {
    }
}
