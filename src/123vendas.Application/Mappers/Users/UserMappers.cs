using _123vendas.Application.Commands.Users;
using _123vendas.Application.DTOs.Users;
using _123vendas.Application.Results.Users;
using _123vendas.Domain.Entities;
using AutoMapper;

namespace _123vendas.Application.Mappers.Users;

public static class UserMappers
{
    private static readonly IMapper _mapper = new MapperConfiguration(cfg =>
        cfg.AddProfile<UserProfile>()).CreateMapper();

    public static User ToEntity(this CreateUserCommand command)
        => _mapper.Map<User>(command);

    public static User ToEntity(this GetUserCommand command)
        => _mapper.Map<User>(command);

    public static GetUserResult ToGetResult(this User entity)
        => _mapper.Map<GetUserResult>(entity);

    public static CreateUserResult ToCreateResult(this User entity)
        => _mapper.Map<CreateUserResult>(entity);

    public static CreateUserCommand ToCommand(this UserPostRequestDTO dto)
        => _mapper.Map<CreateUserCommand>(dto);
}