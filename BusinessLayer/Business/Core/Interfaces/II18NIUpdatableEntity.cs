namespace Business.Core.Interfaces
{
    // ReSharper disable once IdentifierTypo
    /// <summary>
    /// Defines contract for updating instances an Entity that is subject of internationalization
    /// </summary>
    /// <typeparam name="TEntity">Business entity type</typeparam>
    public interface II18NUpdatableEntity<in TEntity>
    {
        /// <summary>
        /// Updates an <see cref="TEntity"/> business entity
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/> entity</param>
        /// <param name="languageIdentifier">Identifier for the language in which the content should be updated</param>
        void Update(TEntity entity, int languageIdentifier);
    }
}
