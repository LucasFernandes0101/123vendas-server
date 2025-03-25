using _123vendas.Application.Commands.Auth;
using _123vendas.Application.Results.Auth;
using _123vendas.Domain.Exceptions;
using _123vendas.Domain.Interfaces.Common;
using _123vendas.Domain.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace _123vendas.Application.Handlers.Auth;

public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IValidator<AuthenticateUserCommand> _validator;

    public AuthenticateUserHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IValidator<AuthenticateUserCommand> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
    }

    public async Task<AuthenticateUserResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequestAsync(request, cancellationToken);

        var user = await _userRepository.GetActiveByEmailAsync(request.Email!);

        if (user is null || !_passwordHasher.VerifyPassword(request.Password!, user.Password!))
            throw new UnauthorizedUserException("Email or password is invalid.");

        var token = await _jwtTokenGenerator.GenerateTokenAsync(user);

        return new AuthenticateUserResult
        {
            Id = user.Id,
            Token = token,
            Email = user.Email,
            Username = user.Username,
            Role = user.Role.ToString()
        };
    }

    private async Task ValidateRequestAsync(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(request, cancellationToken);

        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);
    }
}