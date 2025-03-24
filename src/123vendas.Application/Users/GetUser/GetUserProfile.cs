using _123vendas.Domain.Entities;
using AutoMapper;

namespace _123vendas.Application.Users.GetUser;

public class GetUserProfile : Profile
{
    public GetUserProfile()
    {
        CreateMap<GetUserCommand, User>().ReverseMap();
        CreateMap<GetUserResult, User>().ReverseMap();
    }
}