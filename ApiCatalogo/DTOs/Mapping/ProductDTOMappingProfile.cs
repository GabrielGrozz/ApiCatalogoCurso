using AutoMapper;
using Catalog.Models;

namespace ApiCatalogo.DTOs.Mapping
{
    public class ProductDTOMappingProfile : Profile
    {
        public ProductDTOMappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
        }
    }
}
