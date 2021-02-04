using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Business.Core.Data;
using Business.SearchFilters;
using CrossCutting.Exceptions;
using CrossCutting.SearchFilters.DataAccess;
using Dapper;
using Serilog;

namespace Data.AccessObjects.Product
{
    public partial class ProductDao : BaseDao, IProductDao
    {
        public ProductDao(ILogger logger, IDbConnection dbConnection, IPagedQueryBuilder pagedQueryBuilder) : base(logger, dbConnection, pagedQueryBuilder)
        {
        }

        /// <summary>
        /// Gets a list of Products
        /// </summary>
        /// <param name="searchFilter">Filtering and ordering restrictions</param>
        /// <returns>A list of Products</returns>
        public IEnumerable<Business.Entities.Product> Get(ProductSearchFilter searchFilter)
        {
            return ExecutePagedQuery<Business.Entities.Product>(GetGetQuery(searchFilter, out object parameters), searchFilter, parameters);
        }

        /// <summary>
        /// Updates a Product with the specified information
        /// </summary>
        /// <param name="productDto">Patch object containing the new Product value</param>
        /// <returns>The modified Product object</returns>
        public void Update(Business.Entities.Product productDto)
        {
            try
            {
                BeginTransaction();

                int rows = DbConnection.Execute
                (
                    @"UPDATE Product
                SET Name = ISNULL(@Name, Name), 
                    UpdatedOn = GETUTCDATE(),
                    UpdatedBy = ISNULL(@UserName, UpdatedBy)
                WHERE ID = @Id",
                    new
                    {
                        productDto.Name,
                        UserName = productDto.UpdatedBy
                    },
                    CurrentTransaction
                );

                if (rows == 0)
                {
                    throw new EntityNotFoundException();
                }

                CommitTransaction();
            }
            catch (Exception)
            {
                RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Creates a new Product 
        /// </summary>
        /// <param name="productDto">The new entity description object</param>
        /// <returns>The newly created Product</returns>  
        public int Create(Business.Entities.Product productDto)
        {
            try
            {
                BeginTransaction();

                string insertSql = @"/* ProductDAO - Create */
                
                INSERT INTO Product (
                   Name, CreatedBy, UpdatedBy
                ) 
                values 
                (
                    @Name, @UserName, @UserName
                ) 

                SELECT @@IDENTITY cast bigint
";

                int newId = DbConnection.ExecuteScalar<int>
                (
                    insertSql,
                    new
                    {
                        productDto.Name,
                        UserName = productDto.CreatedBy
                    },
                    CurrentTransaction
                );

                CommitTransaction();
                return newId;
            }
            catch (Exception)
            {
                RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Gets an Case by it's unique identifier
        /// </summary>
        /// <param name="id">The case unique identifier</param>
        /// <returns>Case with the specified unique identifier</returns>
        public Business.Entities.Product GetById(int id)
        {
            Business.Entities.Product Product = DbConnection.Query<Business.Entities.Product> (QueryGetByIdentifier, new { Id = id }, CurrentTransaction).FirstOrDefault();

            if (Product == null)
            {
                return null;
            }

            return Product;
        }

    }
}
