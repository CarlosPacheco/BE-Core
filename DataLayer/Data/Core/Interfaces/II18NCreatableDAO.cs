using Business.Entities;

namespace Data.Core.Interfaces
{
    public interface II18NCreatableDao<out TIdentifier, in TEntity> 
        where TEntity : IEntity 
        where TIdentifier : struct
    {
        /// <summary>
        /// Creates a new business entity instance
        /// </summary>
        /// <param name="entity">Business entity object</param>
        /// <param name="languageIdentifier">Identifier for the language in which the content should be created</param>
        /// <returns>The newly created business entity unique identifier</returns>
        TIdentifier Create(TEntity entity, int languageIdentifier);
    }
}