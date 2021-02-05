using System;
using System.Data;
using Business.Core.Data.Interfaces;
using CrossCutting.Security.Identity;
using Serilog;

namespace Business.Core
{
    /// <summary>
    /// Describes base behaviour for business entities logic controllers
    /// </summary>
    /// <typeparam name="TDataAccessObject">Business entity corresponding data access object type</typeparam>
    public class BaseBlo<TDataAccessObject> : IDisposable, IBaseBlo<TDataAccessObject>
        where TDataAccessObject : IBaseDao
    {
        public TDataAccessObject DataAccess { get; set; }

        public IDbTransaction CurrentTransaction => DataAccess.CurrentTransaction;

        public IAuthorization Authorization { get; set; }

        /// <summary>
        /// Logger instance
        /// </summary>
        protected ILogger Logger { get; }

        public BaseBlo(TDataAccessObject dataAccess, ILogger logger)
        {
            DataAccess = dataAccess;
            Logger = logger;
        }

        public BaseBlo(TDataAccessObject dataAccess, IAuthorization authorization, ILogger logger) : this(dataAccess, logger)
        {
            Authorization = authorization;
        }

        public BaseBlo(TDataAccessObject dataAccess, IAuthorization authorization, IDbTransaction dbTransaction, ILogger logger) : this(dataAccess, authorization, logger)
        {
            DataAccess.SetDbTransaction(dbTransaction);
        }

        public BaseBlo(TDataAccessObject dataAccess, IAuthorization authorization, IDbConnection dbConnection, ILogger logger) : this(dataAccess, authorization, logger)
        {
            DataAccess.SetDbConnection(dbConnection);
        }

        public IDbTransaction BeginTransaction()
        {
            return DataAccess.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return DataAccess.BeginTransaction(isolationLevel);
        }

        public void CommitTransaction()
        {
            DataAccess.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            DataAccess.RollbackTransaction();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DataAccess.Dispose();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~BaseBlo()
        {
            Dispose(false);
        }
    }
}
