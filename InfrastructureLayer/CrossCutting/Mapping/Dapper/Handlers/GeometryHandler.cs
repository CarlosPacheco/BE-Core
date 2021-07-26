using System;
using System.Data;
using Dapper;
using NetTopologySuite.Geometries;
using Npgsql;
using NpgsqlTypes;

namespace CrossCutting.Mapping.Dapper
{
    /// <summary>
    /// Type-handler for the SqlGeography spatial type
    /// </summary>
    public class GeometryHandler : SqlMapper.TypeHandler<Geometry>
    {
        /// <summary>
        /// Create a new handler instance
        /// </summary>
        protected GeometryHandler() { }

        /// <summary>
        /// Default handler instance
        /// </summary>
        public static readonly GeometryHandler Default = new GeometryHandler();

        /// <summary>
        /// Assign the value of a parameter before a command executes
        /// </summary>
        /// <param name="parameter">The parameter to configure</param>
        /// <param name="value">Parameter value</param>
        public override void SetValue(IDbDataParameter parameter, Geometry value)
        {
            parameter.Value = (object)value ?? DBNull.Value;

            if (parameter is NpgsqlParameter npgsqlParameter)
            {
                npgsqlParameter.NpgsqlDbType = NpgsqlDbType.Geography;
                npgsqlParameter.NpgsqlValue = value;
            }
        }

        /// <summary>
        /// Parse a database value back to a typed value
        /// </summary>
        /// <param name="value">The value from the database</param>
        /// <returns>The typed value</returns>
        public override Geometry Parse(object value)
        {
            return value == null || value is DBNull ? null : (Geometry)value;
        }
    }
}
