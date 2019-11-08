using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Data.Core
{
    public class CoreDao : BaseDao
    {
        /// <summary>
        /// The default connection string key on a web/app configuration XML file
        /// </summary>
        private const string DefaultConnectionStringKey = @"LocalAlda";

        /// <summary>
        /// Instance database connection string key name.
        /// </summary>
        protected string ConnectionStringName { get; } = DefaultConnectionStringKey;

        /// <summary>
        /// Application Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Database connection.
        /// </summary>
        private IDbConnection _dbConnection;

        /// <summary>
        /// Gets the database connection object.
        /// </summary>
        public override IDbConnection DbConnection
        {
            get
            {
                try
                {
                    if (_dbConnection == null)
                    {
                        string connectionString = Configuration.GetConnectionString(ConnectionStringName);

                        if (string.IsNullOrWhiteSpace(connectionString))
                        {
                            throw new ApplicationException("No connection strings found on configuration file");
                        }

                        _dbConnection = new SqlConnection(connectionString);
                    }

                    if (_dbConnection.State != ConnectionState.Open)
                    {
                        _dbConnection.Open();
                    }

                    return _dbConnection;
                }
                catch (Exception ex)
                {
                    Logger.Error("[CoreDAO]::Initialize() Error while trying to connect to DB", ex);
                    throw;
                }
            }
        }

        protected CoreDao(ILogger logger, IDbTransaction dbTransaction) : base(logger, dbTransaction)
        {
        }

        protected CoreDao(IConfiguration configuration, ILogger logger, string connectionStringName) : base(logger)
        {
            Configuration = configuration;
            ConnectionStringName = connectionStringName;
        }

        protected CoreDao(IConfiguration configuration, ILogger logger) : base(logger)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Finalizer calls Dispose(false)
        /// </summary>
        ~CoreDao()
        {
            Dispose(false);
        }
    }
}
