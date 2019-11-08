// Don't exist anymore on .NET Core
// https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-2.2#sources

//using System;
//using System.Reflection;
//using System.Web.Http;
//using System.Web.Http.ModelBinding;
//using Microsoft.AspNetCore.Mvc.ModelBinding;

//namespace CrossCutting.WebAPI.Binders.Extensions
//{
//    public static class ParameterBinding
//    {
//        /// <summary>
//        /// Get the ModelBinderProvider from other Assembly dll, in this case CrossCutting.WebAPI 
//        /// </summary>
//        /// <returns></returns>
//        public static IModelBinderProvider GetModelBinderProvider(this ParameterBindingAttribute attribute, params object[] args)
//        {
//            try
//            {
//                string assemblyName = $"{attribute.GetType().Assembly.GetName().Name}.WebAPI";
//                Assembly asm = Assembly.Load(assemblyName);

//                string typeName = $"{attribute.GetType().Namespace}.{attribute.GetType().Name.Replace("Attribute", "Provider")}";
//                Type type = asm.GetType(typeName);

//                return (ModelBinderProvider)Activator.CreateInstance(type, args);
//            }
//            catch (Exception e)
//            {
//                throw new Exception($"Invalid operation. Could not find model binder provider for {attribute.GetType().Name} type.", e);
//            }
//        }
//    }
//}
