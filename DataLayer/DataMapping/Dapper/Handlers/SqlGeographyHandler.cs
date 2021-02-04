using Microsoft.SqlServer.Types;
using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Data.Mapping.Dapper
{
    /// <summary>
    /// Type-handler for the SqlGeography spatial type
    /// </summary>
    public class SqlGeographyHandler : SqlMapper.TypeHandler<SqlGeography>
    {
        /// <summary>
        /// Create a new handler instance
        /// </summary>
        protected SqlGeographyHandler() { }

        /// <summary>
        /// Default handler instance
        /// </summary>
        public static readonly SqlGeographyHandler Default = new SqlGeographyHandler();

        /// <summary>
        /// Assign the value of a parameter before a command executes
        /// </summary>
        /// <param name="parameter">The parameter to configure</param>
        /// <param name="value">Parameter value</param>
        public override void SetValue(IDbDataParameter parameter, SqlGeography value)
        {
            parameter.Value = (object)value ?? DBNull.Value;

            if (parameter is SqlParameter sqlParameter)
            {
                sqlParameter.UdtTypeName = "geography";
            }
        }

        /// <summary>
        /// Parse a database value back to a typed value
        /// </summary>
        /// <param name="value">The value from the database</param>
        /// <returns>The typed value</returns>
        public override SqlGeography Parse(object value)
        {
            return value == null || value is DBNull ? null : (SqlGeography) value;
        }
    }
}
