namespace Business.Core.Interfaces
{
    /// <summary>
    /// Defines contract for an Entity that can be deleted.
    /// </summary>
    /// <typeparam name="TIdentifier">Business entity unique identifier</typeparam>
    public interface II18NDeletableEntity<in TIdentifier> : IDeletableEntity<TIdentifier>
    {
        /// <summary>
        /// Deletes an Entity information in a specific language.
        /// </summary>
        /// <param name="identifier">Business entity unique identifier</param>
        /// <param name="languageIdentifier">Identifier for the language in which the content should be deleted</param>
        void Delete(TIdentifier identifier, int languageIdentifier);
    }
}
