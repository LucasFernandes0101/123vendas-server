using _123vendas.Application.DTOs.Customers;
using _123vendas.Domain.Entities;
using AutoMapper;

namespace _123vendas.Application.Mappers.Customers;

public class CustomerMapperProfile : Profile
{
    public CustomerMapperProfile()
    {
        CreateMap<CustomerGetDetailResponseDTO, Customer>().ReverseMap();

        CreateMap<CustomerGetResponseDTO, Customer>().ReverseMap();

        CreateMap<CustomerPostRequestDTO, Customer>().ReverseMap();

        CreateMap<CustomerPostResponseDTO, Customer>().ReverseMap();

        CreateMap<CustomerPutRequestDTO, Customer>().ReverseMap();

        CreateMap<CustomerPutResponseDTO, Customer>().ReverseMap();
    }
}
