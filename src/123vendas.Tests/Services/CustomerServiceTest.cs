﻿using _123vendas.Application.Services;
using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;
using _123vendas.Domain.Exceptions;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Tests.Mocks.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Linq.Expressions;
using Xunit;

namespace _123vendas.Tests.Services;

[Trait("Customer", "Service")]
public class CustomerServiceTests
{
    [Fact(DisplayName = "Should create customer successfully")]
    public async Task CreateAsync_ShouldCreateCustomer()
    {
        // Arrange
        var repository = Substitute.For<ICustomerRepository>();
        var validator = Substitute.For<IValidator<Customer>>();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(repository, validator, logger);

        var customerMock = new CustomerMock().Generate();
        validator.ValidateAsync(customerMock).Returns(Task.FromResult(new ValidationResult()));
        repository.AddAsync(customerMock).Returns(Task.FromResult(customerMock));

        // Act
        var result = await service.CreateAsync(customerMock);

        // Assert
        result.Should().BeEquivalentTo(customerMock);
        await repository.Received(1).AddAsync(customerMock);
    }

    [Fact(DisplayName = "Should throw ValidationException when customer is invalid")]
    public async Task CreateAsync_ShouldThrowValidationException_WhenInvalid()
    {
        // Arrange
        var repository = Substitute.For<ICustomerRepository>();
        var validator = Substitute.For<IValidator<Customer>>();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(repository, validator, logger);

        var customerMock = new CustomerMock().Generate();
        var validationErrors = new List<ValidationFailure> { new ValidationFailure("Name", "Name is required.") };
        validator.ValidateAsync(customerMock).Returns(Task.FromResult(new ValidationResult(validationErrors)));

        // Act
        Func<Task> act = () => service.CreateAsync(customerMock);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
        await repository.DidNotReceive().AddAsync(Arg.Any<Customer>());
    }

    [Fact(DisplayName = "Should delete customer successfully")]
    public async Task DeleteAsync_ShouldDeleteCustomer()
    {
        // Arrange
        var repository = Substitute.For<ICustomerRepository>();
        var validator = Substitute.For<IValidator<Customer>>();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(repository, validator, logger);

        var customerMock = new CustomerMock().Generate();
        repository.GetByIdAsync(customerMock.Id).Returns(Task.FromResult(customerMock));

        // Act
        await service.DeleteAsync(customerMock.Id);

        // Assert
        await repository.Received(1).DeleteAsync(customerMock);
    }

    [Fact(DisplayName = "Should throw NotFoundException when customer not found on delete")]
    public async Task DeleteAsync_ShouldThrowNotFoundException_WhenCustomerNotFound()
    {
        // Arrange
        var repository = Substitute.For<ICustomerRepository>();
        var validator = Substitute.For<IValidator<Customer>>();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(repository, validator, logger);

        var invalidId = 999;
        repository.GetByIdAsync(invalidId).Returns(Task.FromResult<Customer>(null));

        // Act
        Func<Task> act = () => service.DeleteAsync(invalidId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
        await repository.DidNotReceive().DeleteAsync(Arg.Any<Customer>());
    }

    [Fact(DisplayName = "Should retrieve all customers successfully")]
    public async Task GetAllAsync_ShouldReturnCustomers()
    {
        // Arrange
        var repository = Substitute.For<ICustomerRepository>();
        var validator = Substitute.For<IValidator<Customer>>();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(repository, validator, logger);

        var customers = new List<Customer> { new CustomerMock().Generate(), new CustomerMock().Generate() };
        repository.GetAsync(1, 10, Arg.Any<Expression<Func<Customer, bool>>>()).Returns(Task.FromResult(new PagedResult<Customer>(customers.Count(), customers)));

        // Act
        var result = await service.GetAllAsync(null, null, null, null, null, null, null, null, 1, 10);

        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(customers);
    }

    [Fact(DisplayName = "Should throw InvalidPaginationParametersException when page is less than 1")]
    public async Task GetAllAsync_ShouldThrowInvalidPaginationParametersException_WhenPageIsLessThan1()
    {
        // Arrange
        var repository = Substitute.For<ICustomerRepository>();
        var validator = Substitute.For<IValidator<Customer>>();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(repository, validator, logger);

        // Act
        Func<Task> act = () => service.GetAllAsync(null, null, null, null, null, null, null, null, 0, 10);

        // Assert
        await act.Should().ThrowAsync<InvalidPaginationParametersException>();
    }

    [Fact(DisplayName = "Should retrieve customer by Id successfully")]
    public async Task GetByIdAsync_ShouldReturnCustomer()
    {
        // Arrange
        var repository = Substitute.For<ICustomerRepository>();
        var validator = Substitute.For<IValidator<Customer>>();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(repository, validator, logger);

        var customerMock = new CustomerMock().Generate();
        repository.GetByIdAsync(customerMock.Id).Returns(Task.FromResult(customerMock));

        // Act
        var result = await service.GetByIdAsync(customerMock.Id);

        // Assert
        result.Should().BeEquivalentTo(customerMock);
    }

    [Fact(DisplayName = "Should throw ServiceException when an error occurs retrieving customer by Id")]
    public async Task GetByIdAsync_ShouldThrowServiceException_WhenErrorOccurs()
    {
        // Arrange
        var repository = Substitute.For<ICustomerRepository>();
        var validator = Substitute.For<IValidator<Customer>>();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(repository, validator, logger);

        var invalidId = 999;
        repository.GetByIdAsync(invalidId).Returns(Task.FromException<Customer>(new Exception("Database error")));

        // Act
        Func<Task> act = () => service.GetByIdAsync(invalidId);

        // Assert
        await act.Should().ThrowAsync<ServiceException>();
    }

    [Fact(DisplayName = "Should update customer successfully")]
    public async Task UpdateAsync_ShouldUpdateCustomer()
    {
        // Arrange
        var repository = Substitute.For<ICustomerRepository>();
        var validator = Substitute.For<IValidator<Customer>>();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(repository, validator, logger);
        var existingCustomer = new CustomerMock().Generate();
        var updatedCustomer = new CustomerMock().Generate();

        repository.GetByIdAsync(existingCustomer.Id).Returns(Task.FromResult(existingCustomer));
        validator.ValidateAsync(existingCustomer).Returns(Task.FromResult(new ValidationResult()));
        repository.UpdateAsync(existingCustomer).Returns(Task.FromResult(existingCustomer));

        // Act
        var result = await service.UpdateAsync(existingCustomer.Id, updatedCustomer);

        // Assert
        result.Should().BeEquivalentTo(existingCustomer);
        await repository.Received(1).UpdateAsync(existingCustomer);
    }

    [Fact(DisplayName = "Should throw NotFoundException when customer not found on update")]
    public async Task UpdateAsync_ShouldThrowNotFoundException_WhenCustomerNotFound()
    {
        // Arrange
        var repository = Substitute.For<ICustomerRepository>();
        var validator = Substitute.For<IValidator<Customer>>();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(repository, validator, logger);

        var updatedCustomer = new CustomerMock().Generate();
        var invalidId = 999;
        repository.GetByIdAsync(invalidId).Returns(Task.FromResult<Customer>(null));

        // Act
        Func<Task> act = () => service.UpdateAsync(invalidId, updatedCustomer);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
        await repository.DidNotReceive().UpdateAsync(Arg.Any<Customer>());
    }

    [Fact(DisplayName = "Should throw ValidationException when customer is invalid on update")]
    public async Task UpdateAsync_ShouldThrowValidationException_WhenCustomerIsInvalid()
    {
        // Arrange
        var repository = Substitute.For<ICustomerRepository>();
        var validator = Substitute.For<IValidator<Customer>>();
        var logger = Substitute.For<ILogger<CustomerService>>();
        var service = new CustomerService(repository, validator, logger);

        var existingCustomer = new CustomerMock().Generate();
        var updatedCustomer = new CustomerMock().Generate();
        repository.GetByIdAsync(existingCustomer.Id).Returns(Task.FromResult(existingCustomer));
        var validationErrors = new List<ValidationFailure> { new ValidationFailure("Name", "Name is required.") };
        validator.ValidateAsync(existingCustomer).Returns(Task.FromResult(new ValidationResult(validationErrors)));

        // Act
        Func<Task> act = () => service.UpdateAsync(existingCustomer.Id, updatedCustomer);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
        await repository.DidNotReceive().UpdateAsync(Arg.Any<Customer>());
    }
}
