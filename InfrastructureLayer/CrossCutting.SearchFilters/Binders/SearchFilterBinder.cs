using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace CrossCutting.SearchFilters.Binders
{
    public class SearchFilterBinder : IModelBinder
    {
        public SearchFilterBinder()
        {      
        }

        /// <summary>Binds the model to a value by using the specified controller context and binding context.</summary>
        /// <returns>true if model binding is successful; otherwise, false.</returns>
        /// <param name="bindingContext">The binding context.</param>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null || !typeof(ISearchFilter).IsAssignableFrom(bindingContext.ModelType))
            {
                 throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            // Try to fetch the value of the argument by name
            var valueProviderResult =  bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            // Check if the argument value is null or empty
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

             // All good for now, extract language header from the request object and assign it to the SearchFilter appropriate property
            if (bindingContext.HttpContext.Request.Headers.TryGetValue("X-Content-Language", out Microsoft.Extensions.Primitives.StringValues headers))
            {
                string contentLanguageHeader = headers.FirstOrDefault();

                if (!(bindingContext.Model is ISearchFilter) || string.IsNullOrWhiteSpace(contentLanguageHeader) || !int.TryParse(contentLanguageHeader, out int contentLanguage))
                {
                    bindingContext.ModelState.TryAddModelError(modelName, "Wrong filter object type or invalid request header value for header 'X-Content-Language'");
                }

                ISearchFilter searchFilter = (ISearchFilter)bindingContext.Model;
              //  searchFilter.ContentLanguage = contentLanguage;
                 bindingContext.Result = ModelBindingResult.Success(searchFilter);
            }

            return Task.CompletedTask;
        }
    }
}
