using _123vendas.Application.Commands.Auth;
using _123vendas.Application.DTOs.Auth;
using _123vendas.Application.Results.Auth;
using _123vendas.Domain.Entities;
using AutoMapper;

namespace _123vendas.Application.Mappers.Auth;

public class AuthenticateUserProfile : Profile
{
    public AuthenticateUserProfile()
    {
        CreateMap<AuthenticateUserRequestDTO, AuthenticateUserCommand>().ReverseMap();

        CreateMap<User, AuthenticateUserResult>()
            .ForMember(dest => dest.Token, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

        CreateMap<AuthenticateUserResult, AuthenticateUserResponseDTO>().ReverseMap();
    }
}