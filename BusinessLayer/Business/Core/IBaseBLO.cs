using System.Data;
using CrossCutting.Security.Identity;

namespace Business.Core
{
    /// <summary>
    /// Describes base behaviour for business entities logic controllers
    /// </summary>
    /// <typeparam name="TIDataAccessObject">Business entity corresponding data access object type</typeparam>
    public interface IBaseBlo<TIDataAccessObject> : IBaseBlo
    {
        /// <summary>
        /// Application Request (data layer) access object instance
        /// </summary>
        TIDataAccessObject DataAccess { get; set; }

        /// <summary>
        /// Gets current data access object transaction
        /// </summary>
        IDbTransaction CurrentTransaction { get; }

        /// <summary>
        /// Starts a new database transaction on the <see cref="DataAccess"/> object underlying connection
        /// </summary>
        /// <returns>The database transaction</returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// Begins a database transaction with the specified <see cref="T:System.Data.IsolationLevel" /> value.
        /// </summary>
        /// <param name="isolationLevel">One of the <see cref="T:System.Data.IsolationLevel" /> values.</param>
        /// <returns>The database transaction</returns>
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Commits the current transaction changes to the database
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rollback the current transaction changes to the database
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Security Principal for authorization information access
        /// </summary>
        IAuthorization Authorization { get; set; }
    }

    public interface IBaseBlo
    {
    }
}