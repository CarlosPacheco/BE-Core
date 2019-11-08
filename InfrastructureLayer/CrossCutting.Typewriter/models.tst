#reference "~\ReferencedAssemblies\CrossCutting.Typewriter.dll" 

${
    // Enable extension methods by adding using Typewriter.Extensions.*
    using Typewriter.Extensions.Types;
    using CrossCutting.Typewriter;

    // Uncomment the constructor to change template settings.
    Template(Settings settings)
    {
        settings.IncludeProject("Business.Enums");
        settings.IncludeProject("Data.TransferObjects");
        settings.IncludeProject("Business.Entities");
        settings.OutputFilenameFactory = (file) =>
        {
            // Extract correct type name depending on its type (class, enum, interface, ...)
           string typeName = file.CreateDirectoriesFromTypeName();

           return "..\\..\\..\\FE\\ALDA-UI\\src\\app\\models" + typeName + "\\" + file.Name.Replace(".cs", ".ts"); 
        }; 

    }

    string Extends(Class c) => c.Extends();

    string Implements(Class c) => c.Implements();

    string Imports(Class c) => c.Imports();

    // This custom function will take the type currently being processed and 
    // append a ? next to any type that is nullable
    string TypeIsNullable(Property p) => p.TypeIsNullable();

    string IsNullable(Property p) => p.IsNullable();

    string NameJsonProperty(Property p) => p.NameJsonProperty();

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
