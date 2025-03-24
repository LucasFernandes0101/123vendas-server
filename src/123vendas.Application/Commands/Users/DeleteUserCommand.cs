using MediatR;

namespace _123vendas.Application.Commands.Users;

public record DeleteUserCommand : IRequest
{
    public DeleteUserCommand(int id)
    {
        Id = id;
    }

    public int Id { get; init; }
}