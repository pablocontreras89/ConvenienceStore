using AutoMapper;
using ConvenienceStore.API.DTOs;
using ConvenienceStore.API.Models;

namespace ConvenienceStore.API.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Product, ProductsListDTO>();
            CreateMap<AddNewProductDTO, Product>();
            CreateMap<Product, ProductResponseDTO>();
            CreateMap<UpdateProductDTO, Product>();
        }
    }
}
