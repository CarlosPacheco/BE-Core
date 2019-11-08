using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;

namespace CrossCutting.SearchFilters.Binders
{
    public class SearchFilterBinderProvider : IModelBinderProvider
    {
        public SearchFilterBinderProvider()
        {         
        }

        /// <summary>Finds a binder for the given type.</summary>
        /// <returns>A binder, which can attempt to bind this type. Or null if the binder knows statically that it will never be able to bind the type.</returns>
        /// <param name="configuration">A configuration object.</param>
        /// <param name="modelType">The type of the model to bind against.</param>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.Metadata.ModelType.IsSubclassOf(typeof(ISearchFilter)))
            {
                return null;
            }

            return new BinderTypeModelBinder(typeof(SearchFilterBinder));
        }
    }
}
