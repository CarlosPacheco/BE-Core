using CrossCutting.SearchFilters.DataAccess;
using System;

namespace CrossCutting.SearchFilters.Attributes
{
    /// <summary>
    /// Defines that a property is part of the default sorting for a resource result sets
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultOrderingPropertyAttribute : Attribute
    {
        /// <summary>
        /// Order of the property in the SQL OrderBy statement
        /// </summary>
        public byte Position { get; set; }

        /// <summary>
        /// Direction of the ordering of the property in the SQL OrderBy statement
        /// </summary>
        public SqlOrderDirection Direction { get; set; }
    }
}
