using _123vendas.Application.Commands.Users;
using FluentValidation;

namespace _123vendas.Application.Validators.Users;

public class GetUserCommandValidator : AbstractValidator<GetUserCommand>
{
    public GetUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}