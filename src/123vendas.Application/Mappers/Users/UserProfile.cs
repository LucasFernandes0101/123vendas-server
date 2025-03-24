using _123vendas.Application.Commands.Users;
using _123vendas.Application.DTOs.Users;
using _123vendas.Application.Results.Users;
using _123vendas.Domain.Entities;
using AutoMapper;

namespace _123vendas.Application.Mappers.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserPostRequestDTO, CreateUserCommand>().ReverseMap();

        CreateMap<CreateUserCommand, User>().ReverseMap();
        CreateMap<CreateUserResult, User>().ReverseMap();

        CreateMap<GetUserCommand, User>().ReverseMap();
        CreateMap<GetUserResult, User>().ReverseMap();
    }
}