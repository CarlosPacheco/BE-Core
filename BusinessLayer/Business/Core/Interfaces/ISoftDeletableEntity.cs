namespace Business.Core.Interfaces
{
    /// <summary>
    /// Defines contract for an Entity that can be soft deleted.
    /// </summary>
    /// <typeparam name="TIdentifier">Business entity unique identifier</typeparam>
    public interface ISoftDeletableEntity<in TIdentifier>
        where TIdentifier : struct
    {
        /// <summary>
        /// Marks a business entity as deleted
        /// </summary>
        /// <param name="identifier">Business entity unique identifier</param>
        void SoftDelete(TIdentifier identifier);
    }
}
