namespace Data.Core.Interfaces
{
    // ReSharper disable once IdentifierTypo
    /// <summary>
    /// Defines contract for an Entity that can be updated.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IUpdatableDao<in TEntity>
    {
        /// <summary>
        /// Updates an entity instance data
        /// </summary>
        /// <param name="entity">Entity instance with updated details to apply</param>
        void Update(TEntity entity);
    }
}
