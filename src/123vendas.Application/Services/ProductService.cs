using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;
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
    private readonly IValidator<Product> _validator;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository,
                         IValidator<Product> validator,
                         ILogger<ProductService> logger)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Product> CreateAsync(Product request)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            return await _repository.AddAsync(request);
        }
        catch (Exception ex) when (ex is not ValidationException)
        {
            throw new ServiceException("An error occurred while creating a product.", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var product = await FindProductOrThrowAsync(id);

            await _repository.DeleteAsync(product);
        }
        catch (Exception ex) when (ex is NotFoundException || ex is EntityAlreadyDeletedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while deleting the product.", ex);
        }
    }

    public async Task<List<Product>> GetAllAsync(int? id,
                                                bool? isActive,
                                                string? name,
                                                DateTime? startDate,
                                                DateTime? endDate,
                                                int page = 1,
                                                int maxResults = 10)
    {
        try
        {
            if (page <= 0 || maxResults <= 0)
                throw new InvalidPaginationParametersException("Page number and max results must be greater than zero.");

            var criteria = BuildCriteria(id, isActive, name, startDate, endDate);

            var result = await _repository.GetAsync(page, maxResults, criteria);

            return result.Items;
        }
        catch (Exception ex) when (ex is InvalidPaginationParametersException)
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
            throw new BaseException("An error occurred while retrieving the product.", ex);
        }
    }

    public async Task<Product> UpdateAsync(int id, Product request)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var product = await UpdateProductAsync(id, request);

            return await _repository.UpdateAsync(product);
        }
        catch (Exception ex) when (ex is ValidationException || ex is NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while updating the product.", ex);
        }
    }

    private async Task<Product> UpdateProductAsync(int id, Product request)
    {
        var existingProduct = await FindProductOrThrowAsync(id);

        existingProduct.Name = request.Name;
        existingProduct.Description = request.Description;
        existingProduct.Category = request.Category;
        existingProduct.BasePrice = request.BasePrice;
        existingProduct.IsActive = request.IsActive;

        return existingProduct;
    }

    private Expression<Func<Product, bool>> BuildCriteria(int? id,
                                                         bool? isActive,
                                                         string? name,
                                                         DateTime? startDate,
                                                         DateTime? endDate)
    {
        return b =>
            (!id.HasValue || b.Id == id.Value) &&
            (!isActive.HasValue || b.IsActive == isActive.Value) &&
            (string.IsNullOrEmpty(name) || b.Name.Contains(name)) &&
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
}