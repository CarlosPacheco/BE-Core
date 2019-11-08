using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace CrossCutting.Web.Binders
{
    public class JsonFormDataModelBinder : IModelBinder
    {
        private readonly JsonOptions _options;

        public JsonFormDataModelBinder(IOptions<JsonOptions> options) =>
            _options = options.Value;

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Test if a value is received
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                // Deserialize from string
                string serialized = valueProviderResult.FirstValue;

                // Use custom json options defined in startup if available
                object deserialized = _options?.JsonSerializerOptions == null ?
                    JsonSerializer.Deserialize(serialized, bindingContext.ModelType) :
                    JsonSerializer.Deserialize(serialized, bindingContext.ModelType, _options.JsonSerializerOptions);

                // Set successful binding result
                bindingContext.Result = ModelBindingResult.Success(deserialized);

            }
#if NET451
            return Task.FromResult(0);
#else
            return Task.CompletedTask;
#endif
        }
    }
}
