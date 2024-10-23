using _123vendas.Domain.Entities;
using FluentValidation;

namespace _123vendas.Application.Validations;

public class BranchProductValidator : AbstractValidator<BranchProduct>
{
    public BranchProductValidator()
    {
        RuleFor(bp => bp.BranchId)
            .GreaterThan(0)
            .WithMessage("Branch ID must be greater than zero.");

        RuleFor(bp => bp.ProductId)
            .GreaterThan(0)
            .WithMessage("Product ID must be greater than zero.");

        RuleFor(bp => bp.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(bp => bp.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock quantity must be zero or greater.");
    }
}