using System;

namespace CrossCutting.SearchFilters.Attributes
{
    /// <summary>
    /// Defines that a property should not be considered on the sorting expression processment. 
    /// It will be ignored when passed on the appropriate order by parameter of the query string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrivateFilteringProperty : Attribute
    {
        /// <summary>
        /// Reason why the property is ommited from (public) filtering and it's purpose on the code.
        /// </summary>
        public string Purpose { get; set; }

        public PrivateFilteringProperty(string purpose)
        {
            Purpose = purpose;
        }
    }
}
