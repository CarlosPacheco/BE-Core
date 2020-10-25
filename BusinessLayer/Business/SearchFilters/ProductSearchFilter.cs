using CrossCutting.SearchFilters;
using CrossCutting.SearchFilters.Attributes;
using System;

namespace Business.SearchFilters
{
    /// <summary>
    /// Case Details Search Filter object
    /// </summary>
    public class ProductSearchFilter : SearchFilter
    {
        /// <summary>
        /// Name
        /// </summary>
        [DbTableAlias("CD")]
        [DbTableColumn("NameDummy")]
        public string Name { get; set; }

        /// <summary>
        /// Last update date and time start
        /// </summary>
        [DbTableAlias("CD")]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Last update date and time start
        /// </summary>
        public DateTime? UpdatedOnStart { get; set; }

        /// <summary>
        /// Last update date and time end
        /// </summary>
        public DateTime? UpdatedOnEnd { get; set; }

    }
}
