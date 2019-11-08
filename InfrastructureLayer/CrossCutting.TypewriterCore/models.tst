${
    // Enable extension methods by adding using Typewriter.Extensions.*
    using Typewriter.Extensions.Types;
    
    // Uncomment the constructor to change template settings.
    Template(Settings settings)
    {
        string rootPath = "C:\\Workspace\\IFX-ALDA\\implementation\\FE\\ALDA-UI\\src\\app\\models";

        settings.IncludeProject("Business.Enums");
        settings.IncludeProject("Data.TransferObjects");
        settings.IncludeProject("Business.Entities");
        settings.IncludeProject("Business.SearchFilters");
        settings.IncludeProject("CrossCutting.SearchFilters");
                     
        settings.OutputFilenameFactory = (file) =>
        {

            // Extract correct type name depending on its type (class, enum, interface, ...)
            string typeName = "undefined";
            if (file.Classes.Count > 0) {
                typeName = file.Classes.First().Namespace.Replace("Business.Entities", string.Empty).Replace("Data.TransferObjects", string.Empty).Replace("Business", string.Empty).Replace("CrossCutting", string.Empty);
            } else if (file.Interfaces.Count > 0) {
                typeName = file.Interfaces.First().Namespace.Replace("Business.Entities", string.Empty).Replace("Data.TransferObjects", string.Empty).Replace("Business", string.Empty).Replace("CrossCutting", string.Empty);
            } else if (file.Enums.Count > 0) {
                typeName = file.Enums.First().Namespace.Replace("Business", string.Empty);               
            }

            typeName = SpacesFromCamel(typeName.Replace(".", "\\"));
              
            List<System.IO.DirectoryInfo> root = Split(new System.IO.DirectoryInfo(rootPath + typeName));
         
		    foreach(System.IO.DirectoryInfo dir in root)
            {
                if(!dir.Exists)
                {
                  System.IO.Directory.CreateDirectory(dir.FullName);
                }
            }
          
           return "..\\..\\..\\FE\\ALDA-UI\\src\\app\\models" + typeName + "\\" + file.Name.Replace(".cs", ".ts"); 
        };

    }

    List<System.IO.DirectoryInfo> Split(System.IO.DirectoryInfo path)
    {
        var ret = new List<System.IO.DirectoryInfo>();
        if (path.Parent != null) ret.AddRange(Split(path.Parent));
        ret.Add(path);
        return ret;
    }

    string Extends(Class c)
    {
    if (c.BaseClass!=null)
	    return " extends " + c.BaseClass.ToString();
      else
	     return string.Empty;
    }

    string Implements(Class c)
    {
    var interfaces = c.Interfaces;
    string implements = string.Empty;

      if(c.Interfaces.Count > 0)
      {
        string impl = string.Join(",", c.Interfaces.Where(i => i.Name != "IEntityDTO" && i.Name != "INamableDTO").Select(i => i.Name));
        if(impl.Length > 0)
        {
          implements= " implements " + impl;
        }
      }

      return implements;
    }

    string Imports(Class c){
      List<string> neededImports = propertyImportPath(c);
      if (c.BaseClass != null)
      { 
	      neededImports.Add("\t import { " + c.BaseClass.Name +" } from '" + getImportPath(c.BaseClass.Namespace, c.BaseClass.Name) + "';");
      }

      if(c.Interfaces.Count > 0)
      {
        neededImports.AddRange(c.Interfaces.Where(i => i.Name != "IEntityDTO" && i.Name != "INamableDTO").Select(i => "\t import { " + i.Name + " } from '" + getImportPath(i.Namespace, i.Name) + "';"));
      }

      return String.Join("\n", neededImports.Distinct());
    }

    List<string> propertyImportPath(Class c)
    {
            List<string> imports = new List<string>();

            foreach (Property p in c.Properties)
            {
                //ignore properties with private or SwaggerExcluded
                if(!p.HasSetter || p.Attributes.Any(a => a.Name == "SwaggerExcluded"))
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
                              if(c.Name == t.Name) continue;
                              imports.Add("\t import { " + t.Name + " } from '" + getImportPath(t.Namespace, t.Name) + "';");
                            }
                        }
                    }
                    else
                    {
                      //ignore import the same class (nested self class)
                      if(c.Name == p.Type.ClassName()) continue;

                      imports.Add("\t import { " + p.Type.ClassName() + " } from '" + getImportPath(p.Type.Namespace, p.Type.ClassName()) + "';");
                    }
                }
                else if (p.Type.IsDate)// convert to Moment
                {
                    imports.Add("\t import { Moment } from 'moment';");
                }
            }

      return imports;
    }
     
    string getImportPath(string @namespace, string name)
    {
        return "@models" + SpacesFromCamel(@namespace.Replace("Business.Entities", string.Empty).Replace("Data.TransferObjects", string.Empty).Replace("Business", string.Empty).Replace("CrossCutting", string.Empty).Replace(".", "/")) + "/" + name;
    }

    string TypeFormatted(Property p)
    {
      var type = p.Type;
      if(type.IsDate)
      {
        return "Moment";
      }

      return type.Name;
    }

    // This custom function will take the type currently being processed and 
    // append a ? next to any type that is nullable
    string TypeIsNullable(Property property)
    {
        var type = property.Type;
        if (type.IsNullable)
        {
            return "?";
        }
        else
        {
            return string.Empty;
        }
    }
    string IsNullable(Property property)
    {
     var type = property.Type;
        if (type.IsNullable)
        {
            return " | null";
        }
        else
        {
           return string.Empty;
        }
    }

    /* Transform a C# project directory name to a "typescript/client" directory name (no upper case, etc.)*/
    string SpacesFromCamel(string value)
    {
        if (value.Length > 0)
        {
            var result = new List<char>();
            char[] array = value.ToCharArray();

            for (var i = 0; i < array.Length; i++)
            {
               char item = array[i];
               if (char.IsUpper(item) && result.Count > 0 && (array[i -1] != '/') && (array[i -1] != '\\'))
               {
                result.Add('-');
               }
               result.Add(char.ToLower(item));
            }

            return new string(result.ToArray());
        }
        return value;
    }

    string NameJsonProperty(Property p)
    {
        Attribute attri = p.Attributes.SingleOrDefault(a => a.Name == "JsonProperty");
        if(attri != null)
        {
          return attri.Value;
        }

        return p.name;
    }
}//The do not modify block below is intended for the outputted typescript files... 
//*************************DO NOT MODIFY**************************
//
//THESE FILES ARE AUTOGENERATED WITH TYPEWRITER AND ANY MODIFICATIONS MADE HERE WILL BE LOST
//PLEASE VISIT http://frhagn.github.io/Typewriter/ TO LEARN MORE ABOUT THIS VISUAL STUDIO EXTENSION
//
//*************************DO NOT MODIFY**************************
$Classes(c => (c.Name.ToUpperInvariant().EndsWith("DTO") || c.Name.EndsWith("SearchFilter")) && !c.Attributes.Any(a => a.Name.Equals("ExcludeFromTemplating")))[$Imports $DocComment[
    /**
    * $Summary
    */]
    export class $Name$Extends$Implements {
        $Properties(p => p.HasSetter && !p.Attributes.Any(a => a.Name == "SwaggerExcluded"))[
        $DocComment[
        /**
        * $Summary
        */]
        $NameJsonProperty$TypeIsNullable: $Type[$IsDate[Moment][$Name]]$IsNullable;]
    }]
