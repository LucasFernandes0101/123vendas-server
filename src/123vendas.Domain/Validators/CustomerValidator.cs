using _123vendas.Domain.Entities;
using FluentValidation;

namespace _123vendas.Domain.Validators;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 150).WithMessage("Name must be between 1 and 150 characters.");

        RuleFor(c => c.Document)
            .NotEmpty().WithMessage("Document is required.")
            .Matches(@"^\d{11,14}$").WithMessage("Document must be between 11 and 14 digits.")
            .WithMessage("Document must contain only digits.");

        RuleFor(c => c.Phone)
            .Length(0, 20).WithMessage("Phone must be up to 20 characters.")
            .Matches(@"^\+?\d*$").WithMessage("Phone must contain only digits and optional leading '+'.")
            .When(c => !string.IsNullOrEmpty(c.Phone));

        RuleFor(c => c.Email)
            .EmailAddress().WithMessage("Invalid email format.")
            .When(c => !string.IsNullOrEmpty(c.Email));

        RuleFor(c => c.Address)
            .Length(0, 250).WithMessage("Address must be up to 250 characters.")
            .When(c => !string.IsNullOrEmpty(c.Address));
    }
}