using System;
using System.Data;
using Business.Core.Data.Interfaces;
using CrossCutting.SearchFilters.DataAccess;
using Serilog;

namespace Business.Core.Data
{
    /// <summary>
    /// Defines a base class for all DAO type classes.
    /// Establishes a database connection based on a connection string and exposes it to be consumed.
    /// </summary>
    public partial class BaseDao : IBaseDao
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// PagedQueryBuilder instance
        /// </summary>
        protected IPagedQueryBuilder PagedQueryBuilder { get; }

        /// <summary>
        /// Indicates whether a database connection was passed in constructor or in a setter method.
        /// Injected database connections should not be disposed by instances as it's going to be used somewhere else.
        /// </summary>
        private bool _dbConnectionWasInjected;

        /// <summary>
        /// Indicates whether a database transaction was passed in constructor or in a setter method.
        /// Injected database transaction to avoid parallel transactions
        /// </summary>
        private bool _dbTransactionWasInjected;

        /// <summary>
        /// Database connection.
        /// </summary>
        private IDbConnection _dbConnection;

        /// <summary>
        /// Gets the database connection object.
        /// </summary>
        public virtual IDbConnection DbConnection
        {
            get
            {
                try
                {

                    if (_dbConnection.State != ConnectionState.Open)
                    {
                        _dbConnection.Open();
                    }

                    return _dbConnection;
                }
                catch (Exception ex)
                {
                    Logger.Error("[BaseDAO]::Initialize() Error while trying to connect to DB", ex);
                    throw;
                }
            }
        }

        /// <summary>
        /// Current database transaction
        /// </summary>
        public IDbTransaction CurrentTransaction { get; protected set; }

        /// <summary>
        /// Checks if the underlying connection to the database is in Open state.
        /// </summary>
        /// <returns>True if connection is in Open state, False otherwise</returns>
        public bool IsConnectionOpen() => _dbConnection != null && _dbConnection.State == ConnectionState.Open;

        public BaseDao(ILogger logger, IDbConnection dbConnection, IPagedQueryBuilder pagedQueryBuilder): this(logger)
        {
            PagedQueryBuilder = pagedQueryBuilder;
            _dbConnection = dbConnection;
            _dbConnectionWasInjected = true;
        }

        public BaseDao(ILogger logger, IDbConnection dbConnection): this(logger)
        {
            _dbConnection = dbConnection;
            _dbConnectionWasInjected = true;
        }

        public BaseDao(ILogger logger, IDbTransaction dbTransaction)
        {
            Logger = logger;
            _dbConnection = dbTransaction.Connection;
            CurrentTransaction = dbTransaction;
            _dbConnectionWasInjected = true;
            _dbTransactionWasInjected = true;
        }

        public BaseDao(ILogger logger)
        {
            Logger = logger;
            _dbTransactionWasInjected = false;
            CurrentTransaction = null;
        }

        /// <summary>
        /// Sets the data access object database connection.
        /// </summary>
        /// <param name="dbConnection">Database connection</param>
        public void SetDbConnection(IDbConnection dbConnection)
        {
            // Avoid hard debugs and throw exception on bad usage.
            if (CurrentTransaction != null)
            {
                throw new ApplicationException("A database transaction is set on DAO.");
            }

            if (_dbConnection != null && _dbConnection.State != ConnectionState.Closed)
            {
                _dbConnection.Close();
            }

            _dbConnection?.Dispose();
            _dbConnection = dbConnection;
            CurrentTransaction = null;
            _dbConnectionWasInjected = true;
            _dbTransactionWasInjected = false;
        }

        /// <summary>
        /// Sets the data access object current connection and active transaction
        /// </summary>
        /// <param name="dbTransaction">The database transaction</param>
        public void SetDbTransaction(IDbTransaction dbTransaction)
        {
            SetDbConnection(dbTransaction.Connection);
            _dbTransactionWasInjected = true;
            CurrentTransaction = dbTransaction;
        }

        /// <summary>
        /// Starts a database transaction on the DAO database connection
        /// </summary>
        /// <returns>The created <see cref="IDbTransaction"/></returns>
        public IDbTransaction BeginTransaction()
        {
            if (!_dbTransactionWasInjected && CurrentTransaction == null)
            {
                CurrentTransaction = DbConnection.BeginTransaction();
            }

            return CurrentTransaction;
        }

        /// <summary>
        /// Starts a database transaction on the DAO database connection with the provided isolation level
        /// </summary>
        /// <param name="isolationLevel">Transaction isolation level</param>
        /// <returns>The created <see cref="IDbTransaction"/></returns>
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            if (!_dbTransactionWasInjected && CurrentTransaction == null)
            {
                CurrentTransaction = DbConnection.BeginTransaction(isolationLevel);
            }

            return CurrentTransaction;
        }

        /// <summary>
        /// Commits the current transaction changes to the database
        /// </summary>
        public void CommitTransaction()
        {
            if (_dbTransactionWasInjected)
            {
                return;
            }

            CurrentTransaction?.Commit();
            CurrentTransaction = null;
        }

        /// <summary>
        /// Rollback the current transaction changes to the database
        /// </summary>
        public void RollbackTransaction()
        {
            if (_dbTransactionWasInjected)
            {
                return;
            }

            CurrentTransaction?.Rollback();
            CurrentTransaction = null;
        }

        /// <summary>
        /// Dispose() calls Dispose(true)
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean-up resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            // Free managed resources
            if (!disposing || _dbConnection == null || _dbConnectionWasInjected)
            {
                return;
            }

            if (_dbConnection.State != ConnectionState.Closed)
            {
                _dbConnection.Close();
            }

            _dbConnection.Dispose();
            _dbConnection = null;
            CurrentTransaction = null;
        }

        /// <summary>
        /// Finalizer calls Dispose(false)
        /// </summary>
        ~BaseDao()
        {
            Dispose(false);
        }
    }

}
