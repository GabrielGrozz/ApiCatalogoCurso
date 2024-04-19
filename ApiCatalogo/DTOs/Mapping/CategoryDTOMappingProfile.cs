using AutoMapper;
using Catalog.Models;

namespace ApiCatalogo.DTOs.Mapping
{
    public class CategoryDTOMappingProfile : Profile
    {
        //definicao do mapeamento
        public CategoryDTOMappingProfile() 
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
        }
    }
}
