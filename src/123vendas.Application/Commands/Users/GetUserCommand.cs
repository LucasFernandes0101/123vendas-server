using _123vendas.Application.Results.Users;
using MediatR;

namespace _123vendas.Application.Commands.Users;

public record GetUserCommand : IRequest<GetUserResult>
{
    public GetUserCommand(int id)
    {
        Id = id;
    }

    public int Id { get; init; }
}