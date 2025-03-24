using _123vendas.Application.Users.CreateUser;
using _123vendas.Domain.Entities;
using AutoMapper;

namespace _123vendas.Application.Users.GetUser;

public static class GetUserMappers
{
    private static readonly IMapper _mapper = new MapperConfiguration(cfg =>
        cfg.AddProfile<GetUserProfile>()).CreateMapper();

    public static User ToEntity(this GetUserCommand command)
        => _mapper.Map<User>(command);

    public static GetUserResult ToResult(this User entity)
        => _mapper.Map<GetUserResult>(entity);
}