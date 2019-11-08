namespace Business.Core.Interfaces
{
    // ReSharper disable once IdentifierTypo
    /// <summary>
    /// Defines contract for an Entity that can be updated
    /// </summary>
    /// <typeparam name="TEntity">Business entity type</typeparam>
    public interface IUpdatableEntity<TEntity>
    {
        /// <summary>
        /// Updates an <see cref="TEntity"/> business entity
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/> entity</param>
        TEntity Update(TEntity entity);
    }
}
