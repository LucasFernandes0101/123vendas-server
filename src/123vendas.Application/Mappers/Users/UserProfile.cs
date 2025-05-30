﻿using _123vendas.Application.Commands.Users;
using _123vendas.Application.DTOs.Users;
using _123vendas.Application.Results.Users;
using _123vendas.Domain.Entities;
using AutoMapper;

namespace _123vendas.Application.Mappers.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserAddressGeolocationDTO, UserAddressGeolocation>().ReverseMap();
        CreateMap<UserAddressDTO, UserAddress>().ReverseMap();
        CreateMap<UserNameDTO, UserName>().ReverseMap();

        CreateMap<UserPostRequestDTO, CreateUserCommand>().ReverseMap();

        CreateMap<CreateUserCommand, User>().ReverseMap();
        CreateMap<CreateUserResult, User>().ReverseMap();
        CreateMap<CreateUserResult, UserPostResponseDTO>().ReverseMap();

        CreateMap<GetUserCommand, User>().ReverseMap();
        CreateMap<GetUserResult, User>().ReverseMap();
        CreateMap<GetUserResult, UserGetResponseDTO>().ReverseMap();
    }
}