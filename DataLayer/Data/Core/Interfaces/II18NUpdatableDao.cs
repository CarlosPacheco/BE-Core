namespace Data.Core.Interfaces
{
    public interface II18NUpdateDao<in TEntity>
    {
        /// <summary>
        /// Updates an <see cref="TEntity"/> business entity
        /// </summary>
        /// <param name="entity"><see cref="TEntity"/> entity</param>
        /// <param name="languageIdentifier">Identifier for the language in which the content should be updated</param>
        void Update(TEntity entity, int languageIdentifier);
    }
}
