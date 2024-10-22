using _123vendas.Application.DTOs.Branches;
using _123vendas.Domain.Entities;
using AutoMapper;

namespace _123vendas.Application.Mappers.Branches;

public static class BranchMappers
{
    private static readonly IMapper _mapper = new MapperConfiguration(cfg =>
        cfg.AddProfile<BranchMapperProfile>()).CreateMapper();

    public static List<BranchGetRequestDTO> ToDTO(this List<Branch> entities)
    {
        return _mapper.Map<List<BranchGetRequestDTO>>(entities);
    }

    public static BranchGetDetailResponseDTO ToDetailDTO(this Branch entity)
    {
        return _mapper.Map<BranchGetDetailResponseDTO>(entity);
    }

    public static BranchPostResponseDTO ToPostResponseDTO(this Branch entity)
    {
        return entity is not null ? _mapper.Map<BranchPostResponseDTO>(entity) : new BranchPostResponseDTO();
    }

    public static BranchPutResponseDTO ToPutResponseDTO(this Branch entity)
    {
        return entity is not null ? _mapper.Map<BranchPutResponseDTO>(entity) : new BranchPutResponseDTO();
    }
}