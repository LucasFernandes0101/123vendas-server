using _123vendas.Application.Validators;
using _123vendas.Domain.Enums;
using FluentValidation;

namespace _123vendas.Application.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(user => user.Email).NotEmpty().SetValidator(new EmailValidator());
        RuleFor(user => user.Username).NotEmpty().Length(3, 50);
        RuleFor(user => user.Password).NotEmpty().SetValidator(new PasswordValidator());
        RuleFor(user => user.Phone).NotEmpty().Matches(@"^\+?[1-9]\d{1,14}$");
        RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
        RuleFor(user => user.Role).NotEqual(UserRole.None);
    }
}