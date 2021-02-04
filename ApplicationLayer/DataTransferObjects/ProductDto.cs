using System;
using Business.Entities;
using CrossCutting.Web.Binders;
using Data.TransferObjects.ObjectMapping.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace Data.TransferObjects
{
    [ModelBinder(typeof(JsonFormDataModelBinder))]
    public class ProductDto : MapFrom<Product>
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date and time of creation.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// User who has created the entity.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Last update date and time.
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Last User to update the entity.
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}
