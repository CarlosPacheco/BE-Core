using Business.Entities;

namespace Business.Core.Interfaces
{
    /// <summary>
    /// Defines contract for an Entity that can be created
    /// </summary>
    /// <typeparam name="TEntity">Business entity type</typeparam>
    public interface ICreatableEntity<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Creates a new <see cref="TEntity"/>
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/> entity</param>
        /// <returns>The newly created <see cref="TEntity"/></returns>
        TEntity Create(TEntity entity);
    }
}