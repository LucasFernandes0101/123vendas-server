using _123vendas.Application.Commands.Users;
using _123vendas.Domain.Exceptions;
using _123vendas.Domain.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace _123vendas.Application.Handlers.Users;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<DeleteUserCommand> _validator;
    public DeleteUserHandler(IUserRepository userRepository, IValidator<DeleteUserCommand> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequestAsync(request, cancellationToken);

        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null)
            throw new EntityNotFoundException($"User with ID {request.Id} not found");

        await _userRepository.DeleteAsync(user);
    }

    private async Task ValidateRequestAsync(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request, cancellationToken);

        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);
    }
}