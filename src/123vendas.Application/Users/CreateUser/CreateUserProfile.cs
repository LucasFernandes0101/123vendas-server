using _123vendas.Domain.Entities;
using AutoMapper;

namespace _123vendas.Application.Users.CreateUser;

public class CreateUserProfile : Profile
{
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>().ReverseMap();
        CreateMap<CreateUserResult, User>().ReverseMap();
    }
}