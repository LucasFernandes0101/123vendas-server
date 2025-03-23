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
        CreateMap<Product, ProductGetDetailResponseDTO>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRatingDTO() { Rate = src.Rating, Count = src.RateCount }))
            .ReverseMap();

        CreateMap<Product, ProductGetResponseDTO>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRatingDTO() { Rate = src.Rating, Count = src.RateCount}))
            .ReverseMap();

        CreateMap<Product, ProductPutResponseDTO>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRatingDTO() { Rate = src.Rating, Count = src.RateCount }))
            .ReverseMap();

        CreateMap<Product, ProductPostResponseDTO>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRatingDTO() { Rate = src.Rating, Count = src.RateCount }))
            .ReverseMap();

        CreateMap<ProductPostRequestDTO, Product>().ReverseMap();

        CreateMap<ProductPutRequestDTO, Product>().ReverseMap();
    }
}
