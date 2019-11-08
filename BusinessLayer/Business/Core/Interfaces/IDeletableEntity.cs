namespace Business.Core.Interfaces
{
    /// <summary>
    /// Defines contract for an Entity that can be deleted.
    /// </summary>
    /// <typeparam name="TIdentifier">Business entity unique identifier</typeparam>
    public interface IDeletableEntity<in TIdentifier>
    {
        /// <summary>
        /// Completely deletes a business entity.
        /// This includes the "core" entity and it's i18n'ed information (if exists).
        /// </summary>
        /// <param name="identifier">Business entity unique identifier</param>
        void Delete(TIdentifier identifier);
    }
}
