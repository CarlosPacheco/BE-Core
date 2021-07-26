using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace CrossCutting.Mapping.Dapper
{
    // TODO:: DAPPER maybe already have this inside the dapper core, check it please

    public class UdtParameter : SqlMapper.IDynamicParameters
    {
        private readonly string _paramName;
        private readonly object _value;
        private readonly string _paramType;

        public UdtParameter(string paramName, string typeName, object value)
        {
            _paramName = paramName;
            _paramType = typeName;
            _value = value;
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            SqlCommand sqlCommand = (SqlCommand)command;

            sqlCommand.Parameters.Add(new SqlParameter
            {
                TypeName = _paramType,
                Value = _value,
                ParameterName = _paramName,
                SqlDbType = SqlDbType.Udt
            });
        }
    }
}
