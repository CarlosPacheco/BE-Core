using System;
using Microsoft.AspNetCore.Mvc;

namespace CrossCutting.SearchFilters.Binders
{
    /// <summary>
    /// Attibute for binding <see cref="SearchFilter"/> derived types from a http request URI
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Class)]
    public class SearchFilterBinderAttribute : ModelBinderAttribute
    {
        /// <summary>
        /// Indicates if content i18n header must exist and be valid.
        /// </summary>
        /// <remarks>
        /// An exception will be thrown if <value>X-Content-Language</value> 
        /// header is either not present or has an invalid value.
        /// </remarks>
        public bool DemandI18NHeader { get; set; }

        public SearchFilterBinderAttribute()
        {
            BinderType = typeof(SearchFilterBinder);
            DemandI18NHeader = false;
        }

        public SearchFilterBinderAttribute(bool mandatoryContentLanguage = false)
        {
            BinderType = typeof(SearchFilterBinder);
            DemandI18NHeader = mandatoryContentLanguage;
        }

    }
}
