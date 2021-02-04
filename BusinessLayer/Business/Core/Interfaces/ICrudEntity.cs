using Business.Core.Entities;
using CrossCutting.SearchFilters;
using CrossCutting.SearchFilters.DataAccess;

namespace Business.Core.Interfaces
{
    /// <summary>
    /// Defines contract for an Entity that can be read
    /// </summary>
    /// <typeparam name="TEntity">Business entity type</typeparam>
    /// <typeparam name="TIdentifier">Business entity unique key data type</typeparam>
    /// <typeparam name="TSearchFilter">Business entity search/filter object type</typeparam>
    public interface ICrudEntity<TEntity, in TIdentifier, in TSearchFilter>
        where TEntity : IBaseEntity
        where TIdentifier : struct
        where TSearchFilter : ISearchFilter
    {
        /// <summary>
        /// Creates a new <see cref="TEntity"/>
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/> entity</param>
        /// <returns>The newly created <see cref="TEntity"/></returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// Completely deletes a business entity.
        /// This includes the "core" entity and it's i18n'ed information (if exists).
        /// </summary>
        /// <param name="identifier">Business entity unique identifier</param>
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
        /// Marks a business entity as deleted
        /// </summary>
        /// <param name="identifier">Business entity unique identifier</param>
        void SoftDelete(TIdentifier identifier);

        /// <summary>
        /// Updates an <see cref="TEntity"/> business entity
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/> entity</param>
        TEntity Update(TEntity entity);
    }
}