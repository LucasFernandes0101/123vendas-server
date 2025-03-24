using _123vendas.Application.DTOs.Carts;
using _123vendas.Domain.Entities;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Application.Mappers.Carts;

[ExcludeFromCodeCoverage]
public class CartMapperProfile : Profile
{
    public CartMapperProfile()
    {
        CreateMap<CartGetDetailResponseDTO, Cart>().ReverseMap();
        CreateMap<CartProductGetDetailResponseDTO, CartProduct>().ReverseMap();

        CreateMap<CartGetResponseDTO, Cart>().ReverseMap();
        CreateMap<CartProductGetResponseDTO, CartProduct>().ReverseMap();

        CreateMap<CartPostRequestDTO, Cart>().ReverseMap();
        CreateMap<CartProductPostRequestDTO, CartProduct>().ReverseMap();
        CreateMap<CartPostResponseDTO, Cart>().ReverseMap();
        CreateMap<CartProductPostResponseDTO, CartProduct>().ReverseMap();

        CreateMap<CartPutRequestDTO, Cart>().ReverseMap();
        CreateMap<CartProductPutRequestDTO, CartProduct>().ReverseMap();
        CreateMap<CartPutResponseDTO, Cart>().ReverseMap();
        CreateMap<CartProductPutResponseDTO, CartProduct>().ReverseMap();
    }
}