using AutoMapper;
using Business.Entities;
using Data.TransferObjects;

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