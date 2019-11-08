using Business.Entities;

namespace Business.Core.Interfaces
{
    /// <summary>
    /// Defines contract for creating instances an Entity that is subject of internationalization
    /// </summary>
    /// <typeparam name="TEntity">Business entity type</typeparam>
    public interface II18NCreatableEntity<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Creates a new business entity instance
        /// </summary>
        /// <param name="entity">Business entity object</param>
        /// <param name="languageIdentifier">Identifier for the language in which the content should be created</param>
        /// <returns>The newly created business entity representation</returns>
        TEntity Create(TEntity entity, int languageIdentifier);
    }
}