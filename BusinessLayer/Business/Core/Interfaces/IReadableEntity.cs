using Business.Entities;
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
    public interface IReadableEntity<TEntity, in TIdentifier, in TSearchFilter>
        where TEntity : IEntity
        where TSearchFilter : ISearchFilter
    {
        /// <summary>
        /// Gets an ordered, and optionally filtered, list of <see cref="TEntity"/>
        /// </summary>
        /// <param name="filter">Filter and ordering description object</param>
        /// <returns>An ordered list of <see cref="TEntity"/></returns>
        IPagedList<TEntity> Get(TSearchFilter filter);

        /// <summary>
        /// Gets a <see cref="TEntity"/> instance by it's unique identifier
        /// </summary>
        /// <param name="identifier"><see cref="TEntity"/> unique identifier</param>
        /// <returns>The <see cref="TEntity"/> with the specified unique identifier</returns>
        TEntity GetByIdentifier(TIdentifier identifier);
    }
}