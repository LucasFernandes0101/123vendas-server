using _123vendas.Domain.Entities;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Domain.Validators;

[ExcludeFromCodeCoverage]
public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Title)
            .NotNull()
            .WithMessage("Product title is required.")
            .NotEmpty()
            .WithMessage("Product title cannot be empty.")
            .MaximumLength(150)
            .WithMessage("Product title must not exceed 150 characters.");

        RuleFor(p => p.Description)
            .MaximumLength(500)
            .WithMessage("Product description must not exceed 500 characters.");

        RuleFor(p => p.Category)
            .IsInEnum()
            .WithMessage("Invalid product category.");

        RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(p => p.Rating)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Rating must be greater than or equal to 0.")
            .LessThanOrEqualTo(10)
            .WithMessage("Rating must be less than or equal to 10.");

        RuleFor(p => p.RateCount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Rate count must be greater than or equal to 0.");

        RuleFor(p => p.Image)
            .NotNull()
            .NotEmpty()
            .WithMessage("Product image is required.");
    }
}