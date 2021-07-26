using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace CrossCutting.Mapping.DatabaseColumns
{
    /// <summary>
    /// Uses the Name value of the <see cref="ColumnMapping"/> specified to determine
    /// the association between the name of the column in the query results and the member to
    /// which it will be extracted. If no column mapping is present all members are mapped as
    /// usual.
    /// </summary>
    public class ColumnAttributeTypeMapper : CustomColumnTypeMapper
    {
        public ColumnAttributeTypeMapper(Type type) : base(GetMappers(type)) { }

        private static IEnumerable<SqlMapper.ITypeMap> GetMappers(Type typeToMap)
        {
            return new SqlMapper.ITypeMap[] {
                new CustomPropertyTypeMap(typeToMap, (type, columnName) => type.GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(false).OfType<ColumnMapping>().Any(attr => attr.Name == columnName))),
                new CustomPropertyTypeMap(typeToMap, (type, name) =>
                {
                    string prefix = typeToMap.Name + "_";
                    if (name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        name = name.Substring(prefix.Length);
                    }
                    return type.GetProperty(name);
                }),
                new DefaultTypeMap(typeToMap)
            };
        }
    }
}