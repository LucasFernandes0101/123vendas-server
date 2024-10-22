﻿using _123vendas.Domain.Entities;
using FluentValidation;

namespace _123vendas.Application.Validations;

public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        RuleFor(branch => branch.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.");

        RuleFor(branch => branch.Address)
            .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

        RuleFor(branch => branch.Phone)
            .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters.")
            .Matches(@"^\+?\d*$").WithMessage("Phone must be a valid phone number.");

        RuleFor(branch => branch.IsActive)
            .NotNull().WithMessage("IsActive is required.");
    }
}