using _123vendas.Application.DTOs.Sales;
using _123vendas.Domain.Entities;
using AutoMapper;

namespace _123vendas.Application.Mappers.Sales;

public class SaleMapperProfile : Profile
{
	public SaleMapperProfile()
	{
        CreateMap<SaleItemGetDTO, SaleItem>().ReverseMap();
        CreateMap<SaleItemPostDTO, SaleItem>().ReverseMap();

        CreateMap<SaleGetDetailResponseDTO, Sale>().ReverseMap();
        CreateMap<SaleGetResponseDTO, Sale>().ReverseMap();
        CreateMap<SalePutRequestDTO, Sale>().ReverseMap();
        CreateMap<SalePutResponseDTO, Sale>().ReverseMap();
        CreateMap<SalePostRequestDTO, Sale>().ReverseMap();
        CreateMap<SalePostResponseDTO, Sale>().ReverseMap();
    }
}