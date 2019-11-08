using System.Data;
using Dapper;
using Oracle.ManagedDataAccess.Client;

namespace Data.Mapping.Dapper.Oracle
{
    public class UdtParameter : SqlMapper.IDynamicParameters
    {
        private readonly string _paramName;
        private readonly object _value;
        private readonly string _udtName;

        public UdtParameter(string paramName, string udtName, object value)
        {
            _paramName = paramName;
            _udtName = udtName;
            _value = value;
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            OracleCommand sqlCommand = (OracleCommand)command;

            sqlCommand.Parameters.Add(new OracleParameter
            {
                ParameterName = _paramName,
                DbType = DbType.Object,
                Direction = ParameterDirection.Input,
                UdtTypeName = _udtName,
                Value = _value
            });
        }
    }

  
}
