using _123vendas.Domain.Entities;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Domain.Validators;

[ExcludeFromCodeCoverage]
public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(item => item.ProductId)
            .GreaterThan(0)
            .WithMessage("ProductId must be greater than zero.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0)
            .WithMessage("UnitPrice must be greater than zero.");

        RuleFor(item => item.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(item => item.Discount)
            .InclusiveBetween(0, 100)
            .When(item => item.Discount.HasValue)
            .WithMessage("Discount must be between 0 and 100.");

        RuleFor(item => item.ProductTitle)
            .NotEmpty()
            .WithMessage("ProductName is required.")
            .MaximumLength(150)
            .WithMessage("ProductName cannot exceed 150 characters.");
    }
}