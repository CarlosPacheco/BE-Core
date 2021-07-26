using System.Collections.Generic;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace CrossCutting.Mapping.Dapper.Oracle
{
    public class OracleDynamicParameters : SqlMapper.IDynamicParameters
    {
        private readonly DynamicParameters dynamicParameters;

        private readonly List<OracleParameter> oracleParameters = new List<OracleParameter>();

        /// <summary>
	    /// construct a dynamic parameter bag
	    /// </summary>
	    public OracleDynamicParameters()
        {
            dynamicParameters = new DynamicParameters();
        }

        /// <summary>
	    /// construct a dynamic parameter bag
	    /// </summary>
	    public OracleDynamicParameters(DynamicParameters dynamicParam)
        {
            dynamicParameters = dynamicParam;
        }

        /// <summary>
        /// construct a dynamic parameter bag
        /// </summary>
        /// <param name="template">can be an anonymous type or a DynamicParameters bag</param>
        public OracleDynamicParameters(object template)
        {
            dynamicParameters = new DynamicParameters(template);
        }

        public OracleDynamicParameters(params string[] refCursorNames) : this()
        {
            AddRefCursorParameters(refCursorNames);
        }

        public OracleDynamicParameters(object template, params string[] refCursorNames) : this(template)
        {
            AddRefCursorParameters(refCursorNames);
        }

        public void AddRefCursorParameters(params string[] refCursorNames)
        {
            foreach (string refCursorName in refCursorNames)
            {
                Add(refCursorName, OracleDbType.RefCursor, ParameterDirection.Output);
            }
        }

        public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction, object value = null, int? size = null)
        {
            OracleParameter oracleParameter;
            if (size.HasValue)
            {
                oracleParameter = new OracleParameter(name, oracleDbType, size.Value, value, direction);
            }
            else
            {
                oracleParameter = new OracleParameter(name, oracleDbType, value, direction);
            }

            oracleParameters.Add(oracleParameter);
        }

        public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction)
        {
            oracleParameters.Add(new OracleParameter(name, oracleDbType, direction));
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            ((SqlMapper.IDynamicParameters)dynamicParameters).AddParameters(command, identity);

            OracleCommand sqlCommand = (OracleCommand)command;

            if (sqlCommand != null)
            {
                sqlCommand.Parameters.AddRange(oracleParameters.ToArray());
            }
        }
    }

}
