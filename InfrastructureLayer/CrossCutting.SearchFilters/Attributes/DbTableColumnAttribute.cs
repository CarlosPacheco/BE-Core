using System;

namespace CrossCutting.SearchFilters.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbTableColumnAttribute : Attribute
    {
        /// <summary>
        /// Database column name corresponding to the decorated property.
        /// </summary>
        public string DbColumnName { get; set; }

        public DbTableColumnAttribute(string dbColumnName)
        {
            DbColumnName = dbColumnName;
        }
    }
}
