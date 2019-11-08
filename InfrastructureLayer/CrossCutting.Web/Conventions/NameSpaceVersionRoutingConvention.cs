using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;
using System.Text.RegularExpressions;

namespace CrossCutting.Web.Conventions
{
    /// <summary>
    /// Grab version number from controller namespace and define route to that controller by combining api prefix, version number and controller name.
    /// </summary>
    public class NameSpaceVersionRoutingConvention : IApplicationModelConvention
    {
        private readonly string _apiPrefix;
        private const string UrlTemplate = "{0}/{1}/{2}";
        public NameSpaceVersionRoutingConvention(string apiPrefix = "api")
        {
            _apiPrefix = apiPrefix;
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var hasRouteAttribute = controller.Selectors.Any(x => x.AttributeRouteModel != null);
                if (hasRouteAttribute)
                {
                    continue;
                }
                string[] nameSpace = controller.ControllerType.Namespace?.Split('.');

                var version = nameSpace?.FirstOrDefault(x => Regex.IsMatch(x, @"[v] [d*]"));
                if (string.IsNullOrEmpty(version))
                {
                    continue;
                }
                controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel()
                {
                    Template = string.Format(UrlTemplate, _apiPrefix, version, controller.ControllerName)
                };
            }
        }
    }
}
