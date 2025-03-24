using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;
using _123vendas.Domain.Exceptions;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace _123vendas.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IValidator<Customer> _validator;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(ICustomerRepository repository,
                         IValidator<Customer> validator,
                         ILogger<CustomerService> logger)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Customer> CreateAsync(Customer request)
    {
        try
        {
            await ValidateCustomerAsync(request);

            return await _repository.AddAsync(request);
        }
        catch (Exception ex) when (ex is ValidationException || ex is BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while creating a customer.", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var customer = await FindCustomerOrThrowAsync(id);

            await _repository.DeleteAsync(customer);
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while deleting the customer.", ex);
        }
    }

    public async Task<PagedResult<Customer>> GetAllAsync(int? id,
                                                  string? name,
                                                  string? document,
                                                  string? phone,
                                                  string? email,
                                                  bool? isActive,
                                                  DateTimeOffset? startDate,
                                                  DateTimeOffset? endDate,
                                                  int page = 1,
                                                  int maxResults = 10,
                                                  string? orderByClause = default)
    {
        try
        {
            if (page <= 0 || maxResults <= 0)
                throw new InvalidPaginationParametersException("Page number and max results must be greater than zero.");

            var criteria = BuildCriteria(id, name, document, phone, email, isActive, startDate, endDate);

            var result = await _repository.GetAsync(page, maxResults, criteria, orderByClause);

            return result;
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while retrieving customers.", ex);
        }
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        try
        {
            var customer = await _repository.GetByIdAsync(id);

            return customer;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while retrieving the customer.", ex);
        }
    }

    public async Task<Customer> UpdateAsync(int id, Customer request)
    {
        try
        {
            var customer = await UpdateCustomerAsync(id, request);

            await ValidateCustomerAsync(customer);

            return await _repository.UpdateAsync(customer);
        }
        catch (Exception ex) when (ex is ValidationException || ex is BaseException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ServiceException("An error occurred while updating the customer.", ex);
        }
    }

    private async Task<Customer> UpdateCustomerAsync(int id, Customer request)
    {
        var existingCustomer = await FindCustomerOrThrowAsync(id);

        existingCustomer.Name = request.Name;
        existingCustomer.Document = request.Document;
        existingCustomer.Phone = request.Phone;
        existingCustomer.Email = request.Email;
        existingCustomer.Address = request.Address;
        existingCustomer.IsActive = request.IsActive;

        return existingCustomer;
    }

    private Expression<Func<Customer, bool>> BuildCriteria(int? id,
                                                           string? name,
                                                           string? document,
                                                           string? phone,
                                                           string? email,
                                                           bool? isActive,
                                                           DateTimeOffset? startDate,
                                                           DateTimeOffset? endDate)
    {
        return b =>
            (!id.HasValue || b.Id == id.Value) &&
            (string.IsNullOrEmpty(name) || b.Name!.Contains(name)) &&
            (string.IsNullOrEmpty(document) || b.Document!.Contains(document)) &&
            (string.IsNullOrEmpty(phone) || b.Phone!.Contains(phone)) &&
            (string.IsNullOrEmpty(email) || b.Email!.Contains(email)) &&
            (!isActive.HasValue || b.IsActive == isActive.Value) &&
            (!startDate.HasValue || b.CreatedAt >= startDate.Value) &&
            (!endDate.HasValue || b.CreatedAt <= endDate.Value);
    }

    private async Task<Customer> FindCustomerOrThrowAsync(int id)
    {
        var customer = await _repository.GetByIdAsync(id);

        if (customer is null)
            throw new NotFoundException($"Customer with ID {id} not found.");

        return customer;
    }

    private async Task ValidateCustomerAsync(Customer customer)
    {
        var validationResult = await _validator.ValidateAsync(customer);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}