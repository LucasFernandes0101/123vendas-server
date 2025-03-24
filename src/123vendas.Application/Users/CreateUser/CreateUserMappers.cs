using _123vendas.Application.DTOs.Sales;
using _123vendas.Domain.Entities;
using AutoMapper;

namespace _123vendas.Application.Users.CreateUser;

public static class CreateUserMappers
{
    private static readonly IMapper _mapper = new MapperConfiguration(cfg =>
        cfg.AddProfile<CreateUserProfile>()).CreateMapper();

    public static User ToEntity(this CreateUserCommand command)
        => _mapper.Map<User>(command);

    public static CreateUserResult ToResult(this User entity)
        => _mapper.Map<CreateUserResult>(entity);
}