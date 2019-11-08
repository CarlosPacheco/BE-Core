using System.Data;
using System.Data.SqlClient;
using CrossCutting.Helpers.Helpers;

namespace CrossCutting.Helpers.Extensions
{
    public static class SqlExtensions
    {
        public static SqlParameter AddOutputParameter(this SqlParameterCollection @params, string parameterName, SqlDbType parameterType)
        {
            SqlParameter sqlParameter = SqlHelper.OutputParameter(parameterName, parameterType);

            @params.Add(sqlParameter);

            return sqlParameter;
        }
    }
}
