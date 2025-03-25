using _123vendas.Domain.Entities;
using FluentValidation;

namespace _123vendas.Domain.Validators;

public class CartValidator : AbstractValidator<Cart>
{
    public CartValidator()
    {
        RuleFor(c => c.UserId)
                .GreaterThan(0)
                .WithMessage("UserId must have a value.");

        RuleFor(c => c.Date)
            .LessThanOrEqualTo(DateTimeOffset.Now)
            .WithMessage("Date cannot be in the future.");

        RuleForEach(cart => cart.Products)
            .SetValidator(new CartProductValidator());
    }
}