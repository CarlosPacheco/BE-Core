using Business.Core.Entities;
using CrossCutting.SearchFilters;
using CrossCutting.SearchFilters.DataAccess;

namespace Business.Core.Data.Interfaces
{
    /// <summary>
    /// Exposes methods for reading a business entity
    /// </summary>
    /// <typeparam name="TEntity">Business entity type</typeparam>
    /// <typeparam name="TIdentifier">Business entity unique key data type</typeparam>
    /// <typeparam name="TSearchFilter">Business entity search/filter object type</typeparam>
    public interface ICrudDAO<TIdentifier, TEntity, in TSearchFilter>
        where TEntity : IBaseEntity
        where TIdentifier : struct
        where TSearchFilter : ISearchFilter
    {
        /// <summary>
        /// Creates a new instance of <see cref="TEntity"/>
        /// </summary>
        /// <param name="entity">Request object</param>
        /// <returns>The newly created <see cref="TEntity"/> unique identifier</returns>
        TIdentifier Create(TEntity entity);

        /// <summary>
        /// Deletes an entity by it's unique identifier.
        /// </summary>
        /// <param name="identifier">Target entity unique identifier</param>
        void Delete(TIdentifier identifier);

        /// <summary>
        /// Gets an ordered, and optionally filtered, list of <see cref="TEntity"/>
        /// </summary>
        /// <param name="filter">Filter and ordering description object</param>
        /// <returns>An ordered list of <see cref="TEntity"/></returns>
        IPaginatedList<TEntity> Get(TSearchFilter filter);

        /// <summary>
        /// Gets a <see cref="TEntity"/> instance by it's unique identifier
        /// </summary>
        /// <param name="identifier"><see cref="TEntity"/> unique identifier</param>
        /// <returns>The <see cref="TEntity"/> with the specified unique identifier</returns>
        TEntity GetByIdentifier(TIdentifier identifier);

        /// <summary>
        /// Soft deletes an entity by it's unique identifier.
        /// This means the record will be kept in the persistence layer but will be marked as
        /// deleted and therefore will be invisible on read actions.
        /// </summary>
        /// <param name="identifier">Target entity unique identifier</param>
        void SoftDelete(TIdentifier identifier);

        /// <summary>
        /// Updates an entity instance data
        /// </summary>
        /// <param name="entity">Entity instance with updated details to apply</param>
        void Update(TEntity entity);
    }
}