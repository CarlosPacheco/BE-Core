using System;
using System.Data;
using Business.Core.Data;
using Business.Core.Data.Interfaces;
using Business.Entities;
using CrossCutting.SearchFilters.DataAccess;
using Dapper;
using Interfaces.Data.AccessObjects;
using Microsoft.Extensions.Logging;

namespace Data.AccessObjects.MultimediaFiles
{
    public partial class MultimediaDao : BaseDao, IMultimediaDao
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MultimediaDao"/>
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbConnection">Database <see cref="IDbConnection"/> to be use with the instance</param>
        public MultimediaDao(ILogger<MultimediaDao> logger, IDbConnection dbConnection, ITransactionManager transactionManager, IPagedQueryBuilder pagedQueryBuilder) : base(logger, dbConnection, transactionManager, pagedQueryBuilder)
        {
        }

        /// <summary>
        /// Gets a <see cref="MultimediaContent"/> instance by it's unique identifier
        /// </summary>
        /// <param name="identifier"><see cref="MultimediaContent"/> unique identifier</param>
        /// <returns>The <see cref="MultimediaContent"/> with the specified unique identifier</returns>
        public Multimedia GetByIdentifier(long identifier)
        {
            return DbConnection.QueryFirstOrDefault<Multimedia>(QueryGetByIdentifier, new { id = identifier }, CurrentTransaction);
        }

        public int Create(Multimedia multimediaContent)
        {
             throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an entity by it's unique identifier.
        /// </summary>
        /// <param name="identifier">Target entity unique identifier</param>
        public void Delete(long identifier)
        {
            throw new NotImplementedException();
        }
    }
}