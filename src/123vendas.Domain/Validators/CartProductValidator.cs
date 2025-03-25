using _123vendas.Domain.Entities;
using FluentValidation;

namespace _123vendas.Domain.Validators;

public class CartProductValidator : AbstractValidator<CartProduct>
{
    public CartProductValidator()
    {
        RuleFor(item => item.ProductId)
            .GreaterThan(0)
            .WithMessage("ProductId must be greater than zero.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
    }
}