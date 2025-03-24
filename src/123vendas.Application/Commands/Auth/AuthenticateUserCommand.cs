using _123vendas.Application.Results.Auth;
using MediatR;

namespace _123vendas.Application.Commands.Auth;

public record AuthenticateUserCommand : IRequest<AuthenticateUserResult>
{
    public string? Email { get; init; }
    public string? Password { get; init; }
}
