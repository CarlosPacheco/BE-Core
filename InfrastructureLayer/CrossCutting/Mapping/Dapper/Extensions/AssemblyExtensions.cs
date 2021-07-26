using System;
using System.Reflection;
using Dapper;
using CrossCutting.Mapping.DatabaseColumns;

namespace CrossCutting.Mapping.Dapper.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Load all Dapper Sql Mapper types from the assembly with the ColumnAttributeTypeMapper Attribute
        /// </summary>
        /// <param name="assembly">The Assembly to load the types</param>
        public static void LoadSqlTypeMaps(this Assembly assembly)
        {
            Type[] types = assembly.GetTypes();

            for (int index = 0; index < types.Length; index++)
            {
                SqlMapper.SetTypeMap(types[index], new ColumnAttributeTypeMapper(types[index]));
            }
        }
    }
}
