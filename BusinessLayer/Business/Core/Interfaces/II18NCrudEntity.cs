using Business.Core.Entities;
using CrossCutting.SearchFilters;
using CrossCutting.SearchFilters.DataAccess;

namespace Business.Core.Interfaces
{
    /// <summary>
    /// Defines contract for creating instances an Entity that is subject of internationalization
    /// </summary>
    /// <typeparam name="TEntity">Business entity type</typeparam>
    /// <typeparam name="TId">Business entity unique key data type</typeparam>
    /// <typeparam name="TSearchFilter">Business entity search/filter object type</typeparam>
    public interface Ii18nCrudEntity<TEntity, in TId, in TSearchFilter>
        where TEntity : IBaseEntity
        where TId : struct
        where TSearchFilter : ISearchFilter
    {
        /// <summary>
        /// Creates a new business entity instance
        /// </summary>
        /// <param name="entity">Business entity object</param>
        /// <param name="languageId">Id for the language in which the content should be created</param>
        /// <returns>The newly created business entity representation</returns>
        TEntity Create(TEntity entity, int languageId);

        /// <summary>
        /// Deletes an Entity information in a specific language.
        /// </summary>
        /// <param name="Id">Business entity unique Id</param>
        /// <param name="languageId">Id for the language in which the content should be deleted</param>
        void Delete(TId Id, int languageId);

        /// <summary>
        /// Updates an <see cref="TEntity"/> business entity
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/> entity</param>
        /// <param name="languageId">Id for the language in which the content should be updated</param>
        void Update(TEntity entity, int languageId);

        /// <summary>
        /// Gets an ordered, and optionally filtered, list of <see cref="TEntity"/>
        /// </summary>
        /// <param name="filter">Filter and ordering description object</param>
        /// <param name="languageId">Id for the language in which the content should be returned</param>
        /// <returns>An ordered list of <see cref="TEntity"/></returns>
        IPaginatedList<TEntity> Get(TSearchFilter filter, int languageId);

        /// <summary>
        /// Gets a <see cref="TEntity"/> instance by it's unique Id
        /// </summary>
        /// <param name="Id"><see cref="TEntity"/> unique Id</param>
        /// <param name="languageId">Id for the language in which the content should be returned</param>
        /// <returns>The <see cref="TEntity"/> with the specified unique Id</returns>
        TEntity GetById(TId Id, int languageId);
    }
}