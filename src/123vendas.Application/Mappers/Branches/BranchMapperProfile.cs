using _123vendas.Application.DTOs.Branches;
using _123vendas.Domain.Entities;

using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Application.Mappers.Branches;

[ExcludeFromCodeCoverage]
public class BranchMapperProfile : Profile
{
    public BranchMapperProfile()
    {
        CreateMap<BranchGetDetailResponseDTO, Branch>().ReverseMap();

        CreateMap<BranchGetResponseDTO, Branch>().ReverseMap();

        CreateMap<BranchPostRequestDTO, Branch>().ReverseMap();

        CreateMap<BranchPostResponseDTO, Branch>().ReverseMap();

        CreateMap<BranchPutRequestDTO, Branch>().ReverseMap();

        CreateMap<BranchPutResponseDTO, Branch>().ReverseMap();
    }
}