using System;

namespace Business.Core.Entities
{
    /// <summary>
    /// Defines common basic properties and methods for a business entity.
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        int? Id { get; set; }

        /// <summary>
        /// Date and time of creation.
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// User who has created the entity.
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Last update date and time.
        /// </summary>
        DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Last User to update the entity.
        /// </summary>
        string UpdatedBy { get; set; }
    }
}
