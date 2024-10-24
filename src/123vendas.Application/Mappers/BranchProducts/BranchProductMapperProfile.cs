using _123vendas.Application.DTOs.BranchProducts;
using _123vendas.Domain.Entities;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Application.Mappers.BranchProducts;

[ExcludeFromCodeCoverage]
public class BranchProductMapperProfile : Profile
{
    public BranchProductMapperProfile()
    {
        CreateMap<BranchProductGetDetailResponseDTO, BranchProduct>().ReverseMap();

        CreateMap<BranchProductGetResponseDTO, BranchProduct>().ReverseMap();

        CreateMap<BranchProductPostRequestDTO, BranchProduct>().ReverseMap();

        CreateMap<BranchProductPostResponseDTO, BranchProduct>().ReverseMap();

        CreateMap<BranchProductPutRequestDTO, BranchProduct>().ReverseMap();

        CreateMap<BranchProductPutResponseDTO, BranchProduct>().ReverseMap();
    }
}