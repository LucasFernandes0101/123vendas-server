using MediatR;

namespace _123vendas.Application.Users.GetUser;

public record GetUserCommand : IRequest<GetUserResult>
{
    public GetUserCommand(int id)
    {
        Id = id;
    }

    public int Id { get; init; }
}