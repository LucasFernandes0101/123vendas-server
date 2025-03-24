using _123vendas.Domain.Enums;
using MediatR;

namespace _123vendas.Application.Users.CreateUser;

public record CreateUserCommand : IRequest<CreateUserResult>
{
    public string? Username { get; init; }
    public string? Password { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
    public UserStatus? Status { get; init; }
    public UserRole? Role { get; init; }
}