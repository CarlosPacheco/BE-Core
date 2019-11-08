using Business.Entities;

namespace Data.Core.Interfaces
{
    public interface ICreatableDao<out TIdentifier, in TEntity> 
        where TEntity : IEntity 
        where TIdentifier : struct
    {
        /// <summary>
        /// Creates a new instance of <see cref="TEntity"/>
        /// </summary>
        /// <param name="entity">Request object</param>
        /// <returns>The newly created <see cref="TEntity"/> unique identifier</returns>
        TIdentifier Create(TEntity entity);
    }
}