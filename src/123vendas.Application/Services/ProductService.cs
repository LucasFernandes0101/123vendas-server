﻿using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;
using _123vendas.Domain.Enums;
using _123vendas.Domain.Exceptions;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace _123vendas.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IBranchProductRepository _branchProductRepository;
    private readonly IValidator<Product> _validator;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository,
                          IBranchProductRepository branchProductRepository,
                          IValidator<Product> validator,
                          ILogger<ProductService> logger)
    {
        _repository = repository;
        _branchProductRepository = branchProductRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Product> CreateAsync(Product request)
    {
        try
        {
            await ValidateProductAsync(request);

            return await _repository.AddAsync(request);
        }
        catch (Exception ex) when (ex is ValidationException || ex is BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while creating a product.", ex);
        }
    }

    public async Task<IEnumerable<string>> GetAllCategoriesAsync()
    {
        var categories = Enum.GetValues(typeof(ProductCategory))
                              .Cast<ProductCategory>()
                              .Select(c => c.ToString());

        return await Task.FromResult(categories);
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var product = await FindProductOrThrowAsync(id);

            await _repository.DeleteAsync(product);
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while deleting the product.", ex);
        }
    }

    public async Task<PagedResult<Product>> GetAllAsync(int? id = default,
                                                        bool? isActive = default,
                                                        string? title = default,
                                                        string? category = default,
                                                        decimal? minPrice = default,
                                                        decimal? maxPrice = default,
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

            var criteria = BuildCriteria(id, isActive, title, category, minPrice, maxPrice, startDate, endDate);

            var result = await _repository.GetAsync(page, maxResults, criteria, orderByClause);

            return result;
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while retrieving products.", ex);
        }
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        try
        {
            var product = await _repository.GetByIdAsync(id);

            return product;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while retrieving the product.", ex);
        }
    }

    public async Task<Product> UpdateAsync(int id, Product request)
    {
        try
        {
            var existingProduct = await FindProductOrThrowAsync(id);

            var oldTitle = existingProduct.Title!;
            var oldCategory = existingProduct.Category;

            var product = await UpdateProductAsync(existingProduct, request);

            await ValidateProductAsync(product);

            await _repository.UpdateAsync(product);

            if (!oldTitle.Equals(product.Title) || oldCategory != product.Category)
                await _branchProductRepository.UpdateByProductIdAsync(product.Id, product.Title!, product.Category);

            return product;
        }
        catch (Exception ex) when (ex is ValidationException || ex is BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while updating the product.", ex);
        }
    }

    private async Task<Product> UpdateProductAsync(Product existingProduct, Product request)
    {
        existingProduct.Title = request.Title;
        existingProduct.Description = request.Description;
        existingProduct.Image = request.Image;
        existingProduct.Rating = request.Rating;
        existingProduct.RateCount = request.RateCount;
        existingProduct.Category = request.Category;
        existingProduct.Price = request.Price;
        existingProduct.IsActive = request.IsActive;

        return await Task.FromResult(existingProduct);
    }

    private Expression<Func<Product, bool>> BuildCriteria(int? id,
                                                          bool? isActive,
                                                          string? title,
                                                          string? category,
                                                          decimal? minPrice,
                                                          decimal? maxPrice,
                                                          DateTimeOffset? startDate,
                                                          DateTimeOffset? endDate)
    {
        ProductCategory? categoryFilter = default;

        if (!string.IsNullOrWhiteSpace(category))
            if (Enum.TryParse<ProductCategory>(category, true, out var categoryEnum))
                categoryFilter = categoryEnum;

        return b =>
            (!id.HasValue || b.Id == id.Value) &&
            (!isActive.HasValue || b.IsActive == isActive.Value) &&
            (string.IsNullOrEmpty(title) ||
            (title.StartsWith("*") && title.EndsWith("*") ? b.Title!.Contains(title.Trim('*')) :
            title.StartsWith("*") ? b.Title!.EndsWith(title.TrimStart('*')) :
            title.EndsWith("*") ? b.Title!.StartsWith(title.TrimEnd('*')) :
            b.Title == title)) &&
            (!categoryFilter.HasValue || b.Category == categoryFilter.Value) &&
            (!minPrice.HasValue || b.Price >= minPrice.Value) &&
            (!maxPrice.HasValue || b.Price <= maxPrice.Value) &&
            (!startDate.HasValue || b.CreatedAt >= startDate.Value) &&
            (!endDate.HasValue || b.CreatedAt <= endDate.Value);
    }

    private async Task<Product> FindProductOrThrowAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            throw new NotFoundException($"Product with ID {id} not found.");

        return product;
    }

    private async Task ValidateProductAsync(Product request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}