﻿using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;
using _123vendas.Domain.Exceptions;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace _123vendas.Application.Services;

public class BranchService : IBranchService
{
    private readonly IBranchRepository _repository;
    private readonly IValidator<Branch> _validator;
    private readonly ILogger<BranchService> _logger;

    public BranchService(IBranchRepository repository,
                         IValidator<Branch> validator,
                         ILogger<BranchService> logger)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Branch> CreateAsync(Branch request)
    {
        try
        {
            await ValidateBranchAsync(request);

            return await _repository.AddAsync(request);
        }
        catch (Exception ex) when (ex is not ValidationException)
        {
            throw new ServiceException("An error occurred while creating a branch.", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var branch = await FindBranchOrThrowAsync(id);

            await _repository.DeleteAsync(branch);
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while deleting the branch.", ex);
        }
    }

    public async Task<PagedResult<Branch>> GetAllAsync(int? id = default,
                                                       bool? isActive = default,
                                                       string? name = default,
                                                       DateTimeOffset? startDate = default,
                                                       DateTimeOffset? endDate = default,
                                                       int page = 1,
                                                       int maxResults = 10,
                                                       string? orderByClause = default)
    {
        try
        {
            if (page <= 0 || maxResults <= 0)
                throw new InvalidPaginationParametersException("Page number and max results must be greater than zero.");

            var criteria = BuildCriteria(id, isActive, name, startDate, endDate);

            var result = await _repository.GetAsync(page, maxResults, criteria, orderByClause);

            return result;
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while retrieving branches.", ex);
        }
    }

    public async Task<Branch?> GetByIdAsync(int id)
    {
        try
        {
            var branch = await _repository.GetByIdAsync(id);

            return branch;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while retrieving the branch.", ex);
        }
    }

    public async Task<Branch> UpdateAsync(int id, Branch request)
    {
        try
        {
            var branch = await UpdateBranchAsync(id, request);

            await ValidateBranchAsync(branch);

            return await _repository.UpdateAsync(branch);
        }
        catch (Exception ex) when (ex is ValidationException || ex is BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while updating the branch.", ex);
        }
    }

    private async Task<Branch> UpdateBranchAsync(int id, Branch request)
    {
        var existingBranch = await FindBranchOrThrowAsync(id);

        existingBranch.Name = request.Name;
        existingBranch.Address = request.Address;
        existingBranch.Phone = request.Phone;
        existingBranch.IsActive = request.IsActive;

        return existingBranch;
    }

    private Expression<Func<Branch, bool>> BuildCriteria(int? id,
                                                         bool? isActive,
                                                         string? name,
                                                         DateTimeOffset? startDate,
                                                         DateTimeOffset? endDate)
    {
        return b =>
            (!id.HasValue || b.Id == id.Value) &&
            (!isActive.HasValue || b.IsActive == isActive.Value) &&
            (string.IsNullOrEmpty(name) || b.Name!.Contains(name)) &&
            (!startDate.HasValue || b.CreatedAt >= startDate.Value) &&
            (!endDate.HasValue || b.CreatedAt <= endDate.Value);
    }

    private async Task<Branch> FindBranchOrThrowAsync(int id)
    {
        var branch = await _repository.GetByIdAsync(id);

        if (branch is null)
            throw new NotFoundException($"Branch with ID {id} not found.");

        return branch;
    }

    private async Task ValidateBranchAsync(Branch branch)
    {
        var validationResult = await _validator.ValidateAsync(branch);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}