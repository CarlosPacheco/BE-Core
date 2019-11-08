using AutoMapper;
using Business.Entities.Product;
using Data.TransferObjects.Listings;
using Data.TransferObjects.Product;

namespace Application.ObjectMapping
{
    /// <summary>
    /// Setups the mapping between business entities and data transfer objects
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          CreateMap<Product, ProductDto>().ReverseMap();        
          CreateMap<Product, ListingDto>().ReverseMap();            
        }
    }
}