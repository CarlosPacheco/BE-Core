using System;

namespace CrossCutting.Mapping.DatabaseColumns
{
    /// <summary>
    /// Represents the database column a property is mapped to
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ColumnMapping : Attribute
    {
        /// <summary>
        /// Database column name
        /// </summary>
        public string Name { get; set; }
    }
}