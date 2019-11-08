namespace Data.Core.Interfaces
{
    /// <summary>
    /// Defines contract for an Entity that can be soft deleted.
    /// </summary>
    /// <typeparam name="TIdentifier"></typeparam>
    public interface ISoftDeleteDao<in TIdentifier>
        where TIdentifier : struct
    {
        /// <summary>
        /// Soft deletes an entity by it's unique identifier.
        /// This means the record will be kept in the persistence layer but will be marked as
        /// deleted and therefore will be invisible on read actions.
        /// </summary>
        /// <param name="identifier">Target entity unique identifier</param>
        void SoftDelete(TIdentifier identifier);
    }
}
