namespace Data.Core.Interfaces
{
    public interface IDeletableDao<in TIdentifier>
    {
        /// <summary>
        /// Deletes an entity by it's unique identifier.
        /// </summary>
        /// <param name="identifier">Target entity unique identifier</param>
        void Delete(TIdentifier identifier);
    }
}
