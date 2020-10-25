using System;
using System.Data;
using Business.Core.Data.Interfaces;
using Business.Entities;
using CrossCutting.Security.Identity;

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

        public BaseBlo(TDataAccessObject dataAccess)
        {
            DataAccess = dataAccess;
        }

        public BaseBlo(TDataAccessObject dataAccess, IAuthorization authorization) : this(dataAccess)
        {
            Authorization = authorization;
        }

        public BaseBlo(TDataAccessObject dataAccess, IAuthorization authorization, IDbTransaction dbTransaction) : this(dataAccess, authorization)
        {
            DataAccess.SetDbTransaction(dbTransaction);
        }

        public BaseBlo(TDataAccessObject dataAccess, IAuthorization authorization, IDbConnection dbConnection) : this(dataAccess, authorization)
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


    /// <summary>
    /// Describes base behaviour for business entities logic controllers
    /// </summary>
    /// <typeparam name="TEntity">Business entity type</typeparam>
    /// <typeparam name="TDataAccessObject">Business entity corresponding data access object type</typeparam>
    public abstract class BaseBlo<TEntity, TDataAccessObject> : BaseBlo<TDataAccessObject>
        where TEntity : class, IBaseEntity, new()
        where TDataAccessObject : IBaseDao
    {
        
        /// <summary>
        /// Loads business entities related and referenced by <see cref="TEntity"/>
        /// </summary>
        /// <remarks>Should be overriden whenever there's need to load referenced/nested entities</remarks>
        /// <returns></returns>
        protected virtual TEntity ReferencedEntity { get; set; }

        protected BaseBlo(TDataAccessObject dataAccess) : base(dataAccess)
        {
        }

        protected BaseBlo(TDataAccessObject dataAccess, IAuthorization authorization) : base(dataAccess, authorization)
        {
        }

        protected BaseBlo(TDataAccessObject dataAccess, IAuthorization authorization, IDbTransaction dbTransaction) : base(dataAccess, authorization, dbTransaction)
        {
        }

        protected BaseBlo(TDataAccessObject dataAccess, IAuthorization authorization, IDbConnection dbConnection) : base(dataAccess, authorization, dbConnection)
        {
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
