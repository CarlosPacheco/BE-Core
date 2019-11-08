using System;

namespace CrossCutting.SearchFilters.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class DbTableAliasAttribute : Attribute
    {
        /// <summary>
        /// Table alias for the decorated property.
        /// </summary>
        public string SqlTableAlias { get; set; }

        public DbTableAliasAttribute(string sqlTableAlias)
        {
            SqlTableAlias = sqlTableAlias;
        }
    }
}
