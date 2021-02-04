using Business.Entities;
using Data.TransferObjects.ObjectMapping.Mappings;

namespace Data.TransferObjects
{
    public class ListingDto : MapFrom<Product>
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }   
    }
}
