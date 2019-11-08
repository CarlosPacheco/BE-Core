namespace Data.Core.Interfaces
{
    public interface II18NDeletableDao<in TIdentifier> : IDeletableDao<TIdentifier>
    {
        /// <summary>
        /// Deletes an Entity information in a specific language.
        /// </summary>
        /// <param name="identifier">Business entity unique identifier</param>
        /// <param name="languageIdentifier">Identifier for the language in which the content should be deleted</param>
        void Delete(TIdentifier identifier, int languageIdentifier);
    }
}
