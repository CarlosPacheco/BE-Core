using System.Collections.Generic;
using System.Linq;
using CrossCutting.Typewriter.Attributes;
using Typewriter.CodeModel;
using Typewriter.Extensions.Types;
using Typewriter.Extensions.WebApi;

namespace CrossCutting.Typewriter
{
    public static class Helper
    {
        public static readonly string RootPath = "C:\\Workspace\\IFX-ALDA\\implementation\\FE\\ALDA-UI\\src\\app\\models";

        public static List<System.IO.DirectoryInfo> Split(System.IO.DirectoryInfo path)
        {
            var ret = new List<System.IO.DirectoryInfo>();
            if (path.Parent != null) ret.AddRange(Split(path.Parent));
            ret.Add(path);
            return ret;
        }

        /// <summary>
        /// Transform a C# project directory name to a "typescript/client" directory name (no upper case, etc.)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SpacesFromCamel(string value)
        {
            if (value.Length > 0)
            {
                var result = new List<char>();
                char[] array = value.ToCharArray();

                for (var i = 0; i < array.Length; i++)
                {
                    char item = array[i];
                    if (char.IsUpper(item) && result.Count > 0 && (array[i - 1] != '/') && (array[i - 1] != '\\'))
                    {
                        result.Add('-');
                    }
                    result.Add(char.ToLower(item));
                }

                return new string(result.ToArray());
            }
            return value;
        }

        /// <summary>
        /// Custom extension methods can be used in the template by adding a $ prefix e.g. $LoudName
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string LoudName(this Property property)
        {
            return property.Name.ToUpperInvariant();
        }

        /// <summary>
        /// Extract correct type name depending on its type (class, enum, interface, ...)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetTypeName(this File file)
        {
            if (file.Classes.Count > 0)
            {
                return file.Classes[0].Namespace.Replace("Business.Entities", string.Empty).Replace("Data.TransferObjects", string.Empty);
            }

            if (file.Interfaces.Count > 0)
            {
                return file.Interfaces[0].Namespace.Replace("Business.Entities", string.Empty).Replace("Data.TransferObjects", string.Empty);
            }

            if (file.Enums.Count > 0)
            {
                return file.Enums[0].Namespace.Replace("Business", string.Empty);
            }

            return "undefined";
        }

        /// <summary>
        /// Create the missing Directories from the typeName path
        /// </summary>
        /// <param name="typeName"></param>
        public static string CreateDirectories(string typeName)
        {
            typeName = SpacesFromCamel(typeName.Replace(".", "\\"));

            List<System.IO.DirectoryInfo> root = Split(new System.IO.DirectoryInfo(RootPath + typeName));

            foreach (System.IO.DirectoryInfo dir in root)
            {
                if (!dir.Exists)
                {
                    System.IO.Directory.CreateDirectory(dir.FullName);
                }
            }

            return typeName;
        }

        public static string CreateDirectoriesFromTypeName(this File file)
        {
            string typeName = "undefined";
            if (file.Classes.Count > 0)
            {
                typeName = file.Classes[0].Namespace.Replace("Business.Entities", string.Empty).Replace("Data.TransferObjects", string.Empty);
            }

            if (file.Interfaces.Count > 0)
            {
                typeName =  file.Interfaces[0].Namespace.Replace("Business.Entities", string.Empty).Replace("Data.TransferObjects", string.Empty);
            }

            if (file.Enums.Count > 0)
            {
                typeName =  file.Enums[0].Namespace.Replace("Business", string.Empty);
            }

            typeName = SpacesFromCamel(typeName.Replace(".", "\\"));

            List<System.IO.DirectoryInfo> root = Split(new System.IO.DirectoryInfo(RootPath + typeName));

            foreach (System.IO.DirectoryInfo dir in root)
            {
                if (!dir.Exists)
                {
                    System.IO.Directory.CreateDirectory(dir.FullName);
                }
            }

            return typeName;
        }

        public static string Extends(this Interface c)
        {
            string implements = string.Empty;

            if (c.Interfaces.Count > 0)
            {
                string impl = string.Join(",", c.Interfaces.Where(i => i.Name != "IEntityDTO" && i.Name != "INamableDTO").Select(i => i.Name));
                if (impl.Length > 0)
                {
                    implements = " extends " + impl;
                }
            }

            return implements;
        }

        public static string Imports(this Interface c)
        {
            List<string> neededImports = PropertyImportPath(c);

            if (c.Interfaces.Count > 0)
            {
                neededImports.AddRange(c.Interfaces.Where(i => i.Name != "IEntityDTO" && i.Name != "INamableDTO").Select(i => "\t import { " + i.Name + " } from '" + GetImportPath(i.Namespace, i.Name) + "';"));
            }

            return string.Join("\n", neededImports.Distinct());
        }

        public static List<string> PropertyImportPath(this Interface c)
        {
            List<string> imports = new List<string>();

            foreach (Property p in c.Properties)
            {
                //ignore properties with private or SwaggerExcluded
                if (!p.HasSetter || p.Attributes.Any(a => a.Name == "SwaggerExcluded"))
                {
                    continue;
                }

                if (!p.Type.IsPrimitive || p.Type.IsEnum)
                {
                    if (p.Type.IsGeneric)
                    {
                        if (p.Type.TypeArguments.Count > 0)
                        {
                            foreach (Type t in p.Type.TypeArguments)
                            {
                                //ignore import the same class (nested self class)
                                if (c.Name == t.Name) continue;
                                imports.Add("\t import { " + t.Name + " } from '" + GetImportPath(t.Namespace, t.Name) + "';");
                            }
                        }
                    }
                    else
                    {
                        //ignore import the same class (nested self class)
                        if (c.Name == p.Type.ClassName()) continue;

                        imports.Add("\t import { " + p.Type.ClassName() + " } from '" + GetImportPath(p.Type.Namespace, p.Type.ClassName()) + "';");
                    }
                }
                else if (p.Type.IsDate)// convert to Moment
                {
                    imports.Add("\t import { Moment } from 'moment';");
                }
            }

            return imports;
        }

        public static string GetImportPath(string @namespace, string name)
        {
            return "@models" + SpacesFromCamel(@namespace.Replace("Business.Entities", string.Empty).Replace("Data.TransferObjects", string.Empty).Replace("Business", string.Empty).Replace(".", "/")) + "/" + name;
        }

        // This custom function will take the type currently being processed and 
        // append a ? next to any type that is nullable
        public static string TypeIsNullable(this Property property)
        {
            return property.Type.IsNullable ? "?" : string.Empty;
        }

        public static string IsNullable(this Property property)
        {
            return property.Type.IsNullable ? " | null" : string.Empty;
        }

        public static string Extends(this Class c)
        {
            if (c.BaseClass != null) return " extends " + c.BaseClass;
            return string.Empty;
        }

        public static string Implements(this Class c)
        {
            string implements = string.Empty;

            if (c.Interfaces.Count > 0)
            {
                string impl = string.Join(",", c.Interfaces.Where(i => i.Name != "IEntityDTO" && i.Name != "INamableDTO").Select(i => i.Name));
                if (impl.Length > 0)
                {
                    implements = " implements " + impl;
                }
            }

            return implements;
        }

        public static string Imports(this Class c)
        {
            List<string> neededImports = PropertyImportPath(c);
            if (c.BaseClass != null)
            {
                neededImports.Add("\t import { " + c.BaseClass.Name + " } from '" + GetImportPath(c.BaseClass.Namespace, c.BaseClass.Name) + "';");
            }

            if (c.Interfaces.Count > 0)
            {
                neededImports.AddRange(c.Interfaces.Where(i => i.Name != "IEntityDTO" && i.Name != "INamableDTO").Select(i => "\t import { " + i.Name + " } from '" + GetImportPath(i.Namespace, i.Name) + "';"));
            }

            return string.Join("\n", neededImports.Distinct());
        }

        public static List<string> PropertyImportPath(this Class c)
        {
            List<string> imports = new List<string>();

            foreach (Property p in c.Properties)
            {
                //ignore properties with private or SwaggerExcluded
                if (!p.HasSetter || p.Attributes.Any(a => a.Name == "SwaggerExcluded"))
                {
                    continue;
                }

                if (!p.Type.IsPrimitive || p.Type.IsEnum)
                {
                    if (p.Type.IsGeneric)
                    {
                        if (p.Type.TypeArguments.Count > 0)
                        {
                            foreach (Type t in p.Type.TypeArguments)
                            {
                                //ignore import the same class (nested self class)
                                if (c.Name == t.Name) continue;
                                imports.Add("\t import { " + t.Name + " } from '" + GetImportPath(t.Namespace, t.Name) + "';");
                            }
                        }
                    }
                    else
                    {
                        //ignore import the same class (nested self class)
                        if (c.Name == p.Type.ClassName()) continue;

                        imports.Add("\t import { " + p.Type.ClassName() + " } from '" + GetImportPath(p.Type.Namespace, p.Type.ClassName()) + "';");
                    }
                }
                else if (p.Type.IsDate)// convert to Moment
                {
                    imports.Add("\t import { Moment } from 'moment';");
                }
            }
            return imports;
        }

        public static string TypeFormatted(this Property p)
        {
            var type = p.Type;
            if (type.IsDate)
            {
                return "Moment";
            }

            return type.Name;
        }

        public static string NameJsonProperty(this Property p)
        {
            Attribute attri = p.Attributes.SingleOrDefault(a => a.Name == "JsonProperty");
            if (attri != null)
            {
                return attri.Value;
            }

            return p.name;
        }

        // Change BaseApiController to Service
        public static string ServiceName(this Class c) => c.Name.Replace("Controller", "Service");

        //get the service route
        public static string ServiceRoute(this Class c)
        {
            Attribute attri = c.Attributes.SingleOrDefault(a => a.Name == "Route");
            if (attri != null)
            {
                return attri.Value.Replace("[controller]", c.Name.Replace("Controller", string.Empty).ToLowerInvariant());
            }

            return "missing route attribute";
        }

        // Turn IActionResult into void or void into any
        public static string ReturnType(this Method objMethod)
        {
            if (objMethod.Type.Name == "IActionResult")
            {
                Parameter parameter = objMethod.Parameters.FirstOrDefault(x => !x.Type.IsPrimitive);
                return parameter != null ? parameter.Name : "void";
            }

            if (objMethod.Type.Name == "void")
            {
                Parameter parameter = objMethod.Parameters.FirstOrDefault(x => !x.Type.IsPrimitive);
                return parameter != null ? parameter.Name : "any";
            }

            return objMethod.Type.Name;
        }

        // Get the non primitive paramaters so we can create the Imports at the
        // top of the service
        //public static string ImportsList(Class objClass)
        //{
        //    var ImportsOutput = "";
        //    // Get the methods in the Class
        //    var objMethods = objClass.Methods;
        //    // Loop through the Methdos in the Class
        //    foreach (Method objMethod in objMethods)
        //    {
        //        if (!objMethod.Type.IsPrimitive && !ImportsOutput.Contains(objMethod.Type.ClassName()))
        //        {
        //            ImportsOutput = objMethod.Type.ClassName();
        //        }
        //        // Loop through each Parameter in each method
        //        foreach (Parameter objParameter in objMethod.Parameters)
        //        {
        //            // If the Paramater is not prmitive we need to add this to the Imports
        //            if (!objParameter.Type.IsPrimitive && !ImportsOutput.Contains(objParameter.Name))
        //            {
        //                ImportsOutput = objParameter.Name;
        //            }
        //        }
        //    }
        //    // Notice: As of now this will only return one import
        //    return string.IsNullOrWhiteSpace(ImportsOutput) ? string.Empty : $"import {{ { ImportsOutput } }} from '@app/models';";
        //}

        // Get the non primitive parameters so we can create the Imports at the
        // top of the service
        public static string ImportsList(this Class objClass)
        {
            var importsOutput = "";
            // Get the methods in the Class
            var objMethods = objClass.Methods;
            // Loop through the Methods in the Class
            foreach (Method objMethod in objMethods)
            {
                if (objMethod.Attributes.Any(a => a.GetType() == typeof(TypewriterIgnoreAttribute)))
                {
                    continue;
                }

                if (!objMethod.Type.IsPrimitive && !importsOutput.Contains(objMethod.Type.ClassName()) && !"void".Equals(objMethod.Type.ClassName()))
                {
                    importsOutput = objMethod.Type.ClassName();
                }
                // Loop through each Parameter in each method
                foreach (Parameter objParameter in objMethod.Parameters)
                {
                    // If the Parameter is not primitive we need to add this to the Imports
                    if (!objParameter.Type.IsPrimitive && !importsOutput.Contains(objParameter.Name) && !"void".Equals(objParameter.Name))
                    {
                        importsOutput = objParameter.Name;
                    }
                }
            }
            // Notice: As of now this will only return one import
            return string.IsNullOrWhiteSpace(importsOutput) ? string.Empty : $"import {{ { importsOutput } }} from '@app/models';";
        }

        // Format the method based on the return type
        public static string MethodFormat(this Method objMethod)
        {
            if (objMethod.HttpMethod() == "get")
            {
                return $"<{objMethod.Type.Name}>(API_BASE_URL)";
            }

            if (objMethod.HttpMethod() == "post")
            {
                return $"(API_BASE_URL, {objMethod.Parameters[0].name})";
            }
            if (objMethod.HttpMethod() == "put")
            {
                return $"(API_BASE_URL, {objMethod.Parameters[1].name})";
            }
            if (objMethod.HttpMethod() == "delete")
            {
                return $"(API_BASE_URL)";
            }

            return string.Empty;
        }

        // Change Hub to Service
        public static string HubName(this Class c) => c.Name.Replace("Hub", "HubService");

        // change on to invoke
        public static string InvokeMethodName(this Method m) => "invoke" + m.name.Substring(2);

        public static string PrintDebug(this Class c)
        {
            return c.Name;
        }

        public static string PrintDebug(this Interface i)
        {
            return i.Name;
        }

    }
}
