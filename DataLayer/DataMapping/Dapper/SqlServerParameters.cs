using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Data.Mapping.Dapper
{
    /// <summary>
    /// A bag of parameters that can contain SQL Server specific UDT type parameters 
    /// and can be passed to the Dapper Query and Execute methods
    /// </summary>
    public class SqlServerParameters : DynamicParameters, SqlMapper.IDynamicParameters
    {
        /// <summary>
        /// List of all <see cref="SqlParameter"/> of UDT type to be added just before command execution
        /// </summary>
        private readonly List<SqlParameter> _udtParameters;

        /// <summary>
        /// Creates a new instance of <see cref="SqlServerParameters"/>
        /// </summary>
        /// <param name="udtParameters">List of all <see cref="SqlParameter"/> of UDT type to be added to the command</param>
        public SqlServerParameters(List<SqlParameter> udtParameters)
        {
            _udtParameters = udtParameters;
        }

        /// <summary>
        /// Construct a dynamic parameter bag for Dapper
        /// </summary>
        /// <param name="template">Can be an anonymous type or a <see cref="DynamicParameters"/> bag</param>
        public SqlServerParameters(object template) : base(template)
        {
            _udtParameters = new List<SqlParameter>();
        }

        /// <summary>
        /// Construct a dynamic parameter bag for Dapper
        /// </summary>
        /// <param name="template">Can be an anonymous type or a <see cref="DynamicParameters"/> bag</param>
        /// <param name="udtParameters">List of all <see cref="SqlParameter"/> of UDT type to be added to the command</param>
        public SqlServerParameters(object template, List<SqlParameter> udtParameters) : base(template)
        {
            _udtParameters = udtParameters;
        }

        /// <summary>
        /// Construct a dynamic parameter bag for Dapper
        /// </summary>
        /// <param name="template">Can be an anonymous type or a <see cref="DynamicParameters"/> bag</param>
        /// <param name="udtParameter"><see cref="SqlParameter"/> of UDT type to be added to the command</param>
        public SqlServerParameters(object template, SqlParameter udtParameter) : base(template)
        {
            _udtParameters = new List<SqlParameter>(1) { udtParameter };
        }

        /// <summary>
        /// Add all the parameters needed to the command just before it executes
        /// </summary>
        /// <param name="command">The raw command prior to execution</param>
        /// <param name="identity">Information about the query</param>
        /// <remarks>Implemented explicitly to get behaviour when instance used from base type</remarks>
        void SqlMapper.IDynamicParameters.AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            AddUdtParameters(command);
            AddParameters(command, identity);
        }

        /// <summary>
        /// Adds all <see cref="SqlParameter"/> of UDT type to the list of parameters to be linked on the <see cref="IDbCommand"/>
        /// </summary>
        /// <param name="command"><see cref="IDbCommand"/> object</param>
        private void AddUdtParameters(IDbCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (!(command is SqlCommand))
            {
                return;
            }
            
            foreach (SqlParameter udtParameter in _udtParameters.Where(p => p.SqlDbType == SqlDbType.Udt))
            {
                command.Parameters.Add(udtParameter);
            }
        }

        /// <summary>
        /// Add a <see cref="SqlParameter"/> of UDT type to the list of parameters to be linked on the <see cref="IDbCommand"/>
        /// </summary>
        /// <param name="udtParameter"><see cref="SqlParameter"/> parameter object</param>
        /// <returns><value>True</value> when parameters is of <see cref="SqlDbType.Udt"/>, <value>False</value> otherwise</returns>
        public void AddUdtParameter(SqlParameter udtParameter)
        {
            bool isUdtType = udtParameter.SqlDbType == SqlDbType.Udt;

            if (!isUdtType)
            {
                throw new ArgumentException($"{nameof(udtParameter)} isn't defined as Sql Server UDT");
            }

            _udtParameters.Add(udtParameter);
        }
    }
}
