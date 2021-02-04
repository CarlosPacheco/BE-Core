using Business.Core.Entities;

namespace Business.Core.Interfaces
{
    /// <summary>
    /// Defines methods to take actions on translatable fields of any business entity
    /// that also implements the <see cref="IEntity"/> interface.
    /// </summary>
    /// <typeparam name="TEntity">Business entity type</typeparam>
    /// <typeparam name="TIdentifier">Business entity unique identifier type</typeparam>
    public interface ITranslatableEntity<in TEntity, in TIdentifier>
        where TEntity : IBaseEntity
        where TIdentifier : struct
    {
        /// <summary>
        /// Adds a new translation to the business entity
        /// </summary>
        /// <param name="entity">Business entity containing all translatable properties translated in a specific language</param>
        /// <param name="languageIdentifier">Translation language unique identifier</param>
        void AddTranslation(TEntity entity, int languageIdentifier);

        /// <summary>
        /// Updates a translation of the business entity
        /// </summary>
        /// <param name="entity">Business entity containing all translatable properties translated in a specific language</param>
        /// <param name="languageIdentifier">Translation language unique identifier</param>
        void UpdateTranslation(TEntity entity, int languageIdentifier);

        /// <summary>
        /// Deletes a translation of the business entity
        /// </summary>
        /// <param name="identifier">Business entity unique identifier</param>
        /// <param name="languageIdentifier">Translation language unique identifier</param>
        void DeleteTranslation(TIdentifier identifier, int languageIdentifier);

        /// <summary>
        /// Check if a specific translation exists for a business entity
        /// </summary>
        /// <param name="identifier">Business entity unique identifier</param>
        /// <param name="languageIdentifier">Translation language unique identifier</param>
        /// <returns></returns>
        bool HasTranslation(TIdentifier identifier, int languageIdentifier);

        /// <summary>
        /// Gets a list of languages in which the business entity is translated to.
        /// </summary>
        /// <param name="identifier">Business entity unique identifier</param>
        void GetTranslationLanguages(TIdentifier identifier);

    }
}
