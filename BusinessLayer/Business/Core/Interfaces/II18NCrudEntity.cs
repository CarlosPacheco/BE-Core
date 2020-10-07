using Business.Entities;
using CrossCutting.SearchFilters;
using CrossCutting.SearchFilters.DataAccess;

namespace Business.Core.Interfaces
{
    /// <summary>
    /// Defines contract for creating instances an Entity that is subject of internationalization
    /// </summary>
    /// <typeparam name="TEntity">Business entity type</typeparam>
    /// <typeparam name="TIdentifier">Business entity unique key data type</typeparam>
    /// <typeparam name="TSearchFilter">Business entity search/filter object type</typeparam>
    public interface Ii18nCrudEntity<TEntity, in TIdentifier, in TSearchFilter>
        where TEntity : IBaseEntity
        where TIdentifier : struct
        where TSearchFilter : ISearchFilter
    {
        /// <summary>
        /// Creates a new business entity instance
        /// </summary>
        /// <param name="entity">Business entity object</param>
        /// <param name="languageIdentifier">Identifier for the language in which the content should be created</param>
        /// <returns>The newly created business entity representation</returns>
        TEntity Create(TEntity entity, int languageIdentifier);

        /// <summary>
        /// Deletes an Entity information in a specific language.
        /// </summary>
        /// <param name="identifier">Business entity unique identifier</param>
        /// <param name="languageIdentifier">Identifier for the language in which the content should be deleted</param>
        void Delete(TIdentifier identifier, int languageIdentifier);

        /// <summary>
        /// Updates an <see cref="TEntity"/> business entity
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/> entity</param>
        /// <param name="languageIdentifier">Identifier for the language in which the content should be updated</param>
        void Update(TEntity entity, int languageIdentifier);

        /// <summary>
        /// Gets an ordered, and optionally filtered, list of <see cref="TEntity"/>
        /// </summary>
        /// <param name="filter">Filter and ordering description object</param>
        /// <param name="languageIdentifier">Identifier for the language in which the content should be returned</param>
        /// <returns>An ordered list of <see cref="TEntity"/></returns>
        IPagedList<TEntity> Get(TSearchFilter filter, int languageIdentifier);

        /// <summary>
        /// Gets a <see cref="TEntity"/> instance by it's unique identifier
        /// </summary>
        /// <param name="identifier"><see cref="TEntity"/> unique identifier</param>
        /// <param name="languageIdentifier">Identifier for the language in which the content should be returned</param>
        /// <returns>The <see cref="TEntity"/> with the specified unique identifier</returns>
        TEntity GetByIdentifier(TIdentifier identifier, int languageIdentifier);

    }
}