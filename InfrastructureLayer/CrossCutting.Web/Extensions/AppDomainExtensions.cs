using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;

namespace CrossCutting.Web.Extensions
{
    public static class AppDomainExtensions
    {
        public static AppDomain LoadAllReferenceAssemblies(this AppDomain appDomain)
        {
            List<string> dlls = DependencyContext.Default.CompileLibraries
            .SelectMany(x => x.ResolveReferencePaths())
            .Distinct()
            .Where(x => x.Contains(Directory.GetCurrentDirectory()))
            .ToList();

            foreach (var item in dlls)
            {
                try
                {
                    AssemblyLoadContext.Default.LoadFromAssemblyPath(item);
                }
                catch (FileLoadException)
                {
                } // The Assembly has already been loaded.
                catch (BadImageFormatException)
                {
                } // If a BadImageFormatException exception is thrown, the file is not an assembly.
                catch (Exception)
                {
                }
            }

            return appDomain;
        }


    }
}
