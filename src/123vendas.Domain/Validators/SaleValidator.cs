using _123vendas.Domain.Entities;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Domain.Validators;

[ExcludeFromCodeCoverage]
public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.BranchId)
            .GreaterThan(0)
            .WithMessage("BranchId must be greater than zero.");

        RuleFor(sale => sale.CustomerId)
            .GreaterThan(0)
            .WithMessage("CustomerId must be greater than zero.");

        RuleFor(sale => sale.Date)
            .GreaterThan(DateTime.Now)
            .WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.TotalAmount)
            .GreaterThan(0)
            .WithMessage("TotalAmount must be greater than zero.");

        RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("Sale must contain at least one item.");

        RuleForEach(sale => sale.Items)
            .SetValidator(new SaleItemValidator());
    }
}