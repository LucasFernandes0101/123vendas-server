using _123vendas.Application.DTOs.Products;
using _123vendas.Domain.Entities;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Application.Mappers.Products;

[ExcludeFromCodeCoverage]
public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<ProductGetDetailResponseDTO, Product>().ReverseMap();

        CreateMap<ProductGetResponseDTO, Product>().ReverseMap();

        CreateMap<ProductPostRequestDTO, Product>().ReverseMap();

        CreateMap<ProductPostResponseDTO, Product>().ReverseMap();

        CreateMap<ProductPutRequestDTO, Product>().ReverseMap();

        CreateMap<ProductPutResponseDTO, Product>().ReverseMap();
    }
}
