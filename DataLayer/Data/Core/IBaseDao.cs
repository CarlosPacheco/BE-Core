using System;
using System.Data;

namespace Data.Core
{
    public interface IBaseDao : IDisposable
    {
        /// <summary>
        /// Sql database connection object.
        /// </summary>
        IDbConnection DbConnection { get; }

        /// <summary>
        /// Checks if the underlying connection to the database is in Open state.
        /// </summary>
        /// <returns>True if connection is in Open state, False otherwise</returns>
        bool IsConnectionOpen();

        /// <summary>
        /// Sets the data access object database connection.
        /// </summary>
        /// <param name="dbConnection">Database connection</param>
        void SetDbConnection(IDbConnection dbConnection);

        /// <summary>
        /// Sets the data access object current connection and active transaction
        /// </summary>
        /// <param name="dbTransaction">The database transaction</param>
        void SetDbTransaction(IDbTransaction dbTransaction);

        /// <summary>
        /// Current database transaction
        /// </summary>
        IDbTransaction CurrentTransaction { get; }

        /// <summary>
        /// Begins a database transaction on the DAO database connection
        /// </summary>
        /// <returns>A <see cref="IDbTransaction"/> object</returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// Begins a database transaction with the specified <see cref="T:System.Data.IsolationLevel" /> value.
        /// </summary>
        /// <param name="isolationLevel">One of the <see cref="T:System.Data.IsolationLevel" /> values.</param>
        /// <returns>An object representing the new transaction.</returns>
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Commits the current transaction changes to the database
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rollback the current transaction changes to the database
        /// </summary>
        void RollbackTransaction();
    }
}