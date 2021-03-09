using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using CrossCutting.Helpers.Helpers;

namespace CrossCutting.Helpers.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="DbCommand"/> setup, properties, parameters, etc.
    /// </summary>
    public static class DbParameterCollectionExtensions
    {
        /// <summary>
        /// Adds an input parameter to a <see cref="DbCommand"/>
        /// </summary>
        /// <param name="parameters">Target parameter collection</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterType">Parameter database type</param>
        /// <returns>A new <see cref="SqlParameter"/></returns>
        public static SqlParameter Add(this DbParameterCollection parameters, string parameterName, SqlDbType parameterType)
        {
            SqlParameter param = new SqlParameter(parameterName, parameterType);
            parameters.Add(param);

            return param;
        }

        /// <summary>
        /// Adds an output parameter to a <see cref="DbCommand"/>
        /// </summary>
        /// <param name="parameters">Target parameter collection</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterType">Parameter database type</param>
        /// <returns>A new <see cref="SqlParameter"/></returns>
        public static SqlParameter AddOutputParameter(this DbParameterCollection parameters, string parameterName, SqlDbType parameterType)
        {
            SqlParameter sqlParameter = SqlHelper.OutputParameter(parameterName, parameterType);
            parameters.Add(sqlParameter);

            return sqlParameter;
        }

        /// <summary>
        /// Adds an input parameter to a <see cref="DbCommand"/> with a specified value
        /// </summary>
        /// <param name="parameters">Target parameter collection</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterType">Parameter database type</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>A new <see cref="DbParameter"/></returns>
        public static DbParameter Add(this DbParameterCollection parameters, string parameterName, SqlDbType parameterType, object parameterValue)
        {
            DbParameter parameter = parameters.Add(parameterName, parameterType);
            parameter.Value = parameterValue;

            return parameter;
        }

        /// <summary>
        /// Adds an input parameter to a <see cref="DbCommand"/> with a specified value
        /// </summary>
        /// <param name="parameters">Target parameter collection</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterType">Parameter database type</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>A new <see cref="DbParameter"/></returns>
        public static DbParameter Add(this DbParameterCollection parameters, string parameterName, DbType parameterType, object parameterValue, ParameterDirection direction = ParameterDirection.Input)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = parameterName;
            parameter.DbType = parameterType;
            parameter.Value = parameterValue;
            parameter.Direction = direction;
            parameters.Add(parameter);

            return parameter;
        }
    }
}
