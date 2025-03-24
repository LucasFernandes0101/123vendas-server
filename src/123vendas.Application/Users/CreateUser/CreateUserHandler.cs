using _123vendas.Application.Common.Security;
using _123vendas.Domain.Exceptions;
using _123vendas.Domain.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace _123vendas.Application.Users.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IValidator<CreateUserCommand> _validator;
    public CreateUserHandler(IUserRepository userRepository,
                             IPasswordHasher passwordHasher,
                             IValidator<CreateUserCommand> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _validator = validator;
    }

    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        await ValidateUser(command, cancellationToken);

        var result = await _userRepository.GetAsync(criteria: x => x.Email == command.Email && 
                                                              x.IsActive);

        if (result?.Items is not null && result.Items.Any())
            throw new UserAlreadyExistsException($"User with email {command.Email} already exists");

        var user = command.ToEntity();
        user.Password = _passwordHasher.HashPassword(command.Password!);

        var createdUser = await _userRepository.AddAsync(user);

        return createdUser.ToResult();
    }

    private async Task ValidateUser(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(command, cancellationToken);

        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);
    }
}