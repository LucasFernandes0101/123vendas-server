using _123vendas.Application.Services;
using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;
using _123vendas.Domain.Enums;
using _123vendas.Domain.Exceptions;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Tests.Mocks.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Linq.Expressions;
using Xunit;

namespace _123vendas.Tests.Services
{
    public class ProductServiceTest
    {
        private readonly IProductRepository _productRepository;
        private readonly IBranchProductRepository _branchProductRepository;
        private readonly IValidator<Product> _validator;
        private readonly ILogger<ProductService> _logger;
        private readonly ProductService _productService;

        public ProductServiceTest()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _branchProductRepository = Substitute.For<IBranchProductRepository>();
            _validator = Substitute.For<IValidator<Product>>();
            _logger = Substitute.For<ILogger<ProductService>>();
            _productService = new ProductService(_productRepository, _branchProductRepository, _validator, _logger);
        }

        [Fact(DisplayName = "CreateAsync_Should_Create_Product_Successfully")]
        [Trait("Product", "Service")]
        public async Task CreateAsync_Should_Create_Product_Successfully()
        {
            // Arrange
            var mockProduct = new ProductMock().Generate();
            _validator.ValidateAsync(mockProduct).Returns(Task.FromResult(new ValidationResult()));
            _productRepository.AddAsync(mockProduct).Returns(mockProduct);

            // Act
            var result = await _productService.CreateAsync(mockProduct);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(mockProduct);
            await _productRepository.Received(1).AddAsync(mockProduct);
        }

        [Fact(DisplayName = "CreateAsync_Should_Throw_ValidationException_When_Validation_Fails")]
        [Trait("Product", "Service")]
        public async Task CreateAsync_Should_Throw_ValidationException_When_Validation_Fails()
        {
            // Arrange
            var mockProduct = new ProductMock().Generate();

            _validator.ValidateAsync(mockProduct).Throws(new ValidationException("Validation failed."));

            // Act & Assert
            Func<Task> act = async () => await _productService.CreateAsync(mockProduct);
            await act.Should().ThrowAsync<ValidationException>().WithMessage("Validation failed.");
        }

        [Fact(DisplayName = "DeleteAsync_Should_Delete_Product_Successfully")]
        [Trait("Product", "Service")]
        public async Task DeleteAsync_Should_Delete_Product_Successfully()
        {
            // Arrange
            var mockProduct = new ProductMock().Generate();
            _productRepository.GetByIdAsync(mockProduct.Id).Returns(mockProduct);

            // Act
            await _productService.DeleteAsync(mockProduct.Id);

            // Assert
            await _productRepository.Received(1).DeleteAsync(mockProduct);
        }

        [Fact(DisplayName = "DeleteAsync_Should_Throw_NotFoundException_When_Product_Not_Found")]
        [Trait("Product", "Service")]
        public async Task DeleteAsync_Should_Throw_NotFoundException_When_Product_Not_Found()
        {
            // Arrange
            var invalidId = 999;
            _productRepository.GetByIdAsync(invalidId).Returns((Product)null);

            // Act & Assert
            Func<Task> act = async () => await _productService.DeleteAsync(invalidId);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact(DisplayName = "GetAllAsync_Should_Return_Product_List")]
        [Trait("Product", "Service")]
        public async Task GetAllAsync_Should_Return_Product_List()
        {
            // Arrange
            var mockProducts = new List<Product> { new ProductMock().Generate(), new ProductMock().Generate() };
            _productRepository.GetAsync(1, 10, Arg.Any<Expression<Func<Product, bool>>>())
                .Returns(new PagedResult<Product>(mockProducts.Count, mockProducts));

            // Act
            var result = await _productService.GetAllAsync(null, null, null, null, null, 1, 10);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact(DisplayName = "UpdateAsync_Should_Update_Product_Successfully")]
        [Trait("Product", "Service")]
        public async Task UpdateAsync_Should_Update_Product_Successfully()
        {
            // Arrange
            var mockProduct = new ProductMock().Generate();
            _productRepository.GetByIdAsync(mockProduct.Id).Returns(mockProduct);
            _validator.ValidateAsync(mockProduct).Returns(Task.FromResult(new ValidationResult()));

            // Act
            var result = await _productService.UpdateAsync(mockProduct.Id, mockProduct);

            // Assert
            result.Should().Be(mockProduct);
            await _productRepository.Received(1).UpdateAsync(mockProduct);
        }

        [Fact(DisplayName = "UpdateAsync_Should_Throw_NotFoundException_When_Product_Not_Found")]
        [Trait("Product", "Service")]
        public async Task UpdateAsync_Should_Throw_NotFoundException_When_Product_Not_Found()
        {
            // Arrange
            var mockProduct = new ProductMock().Generate();
            _productRepository.GetByIdAsync(mockProduct.Id).Returns((Product)null);

            // Act & Assert
            Func<Task> act = async () => await _productService.UpdateAsync(mockProduct.Id, mockProduct);
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact(DisplayName = "UpdateAsync_Should_Update_BranchProduct_When_Name_Changes")]
        [Trait("Product", "Service")]
        public async Task UpdateAsync_Should_Update_BranchProduct_When_Name_Changes()
        {
            // Arrange
            var mockProduct = new ProductMock().Generate();
            mockProduct.Name = "Amstel";

            var updatedProduct = new Product
            {
                Id = mockProduct.Id,
                Name = "Heineken",
                Description = mockProduct.Description,
                Category = mockProduct.Category,
                BasePrice = mockProduct.BasePrice,
                IsActive = mockProduct.IsActive
            };

            var productRepository = Substitute.For<IProductRepository>();
            var branchProductRepository = Substitute.For<IBranchProductRepository>();
            var validator = Substitute.For<IValidator<Product>>();
            var logger = Substitute.For<ILogger<ProductService>>();

            productRepository.GetByIdAsync(mockProduct.Id).Returns(mockProduct);
            validator.ValidateAsync(Arg.Any<Product>()).Returns(Task.FromResult(new ValidationResult()));

            var productService = new ProductService(productRepository, branchProductRepository, validator, logger);

            // Act
            await productService.UpdateAsync(mockProduct.Id, updatedProduct);

            // Assert
            await branchProductRepository.Received(1).UpdateByProductIdAsync(mockProduct.Id, updatedProduct.Name, mockProduct.Category);
        }

        [Fact(DisplayName = "UpdateAsync_Should_Update_BranchProduct_When_Category_Changes")]
        [Trait("Product", "Service")]
        public async Task UpdateAsync_Should_Update_BranchProduct_When_Category_Changes()
        {
            // Arrange
            var mockProduct = new ProductMock().Generate();
            mockProduct.Category = ProductCategory.Juice;

            var updatedProduct = new Product
            {
                Id = mockProduct.Id,
                Name = mockProduct.Name,
                Description = mockProduct.Description,
                Category = ProductCategory.Beer,
                BasePrice = mockProduct.BasePrice,
                IsActive = mockProduct.IsActive
            };

            var productRepository = Substitute.For<IProductRepository>();
            var branchProductRepository = Substitute.For<IBranchProductRepository>();
            var validator = Substitute.For<IValidator<Product>>();
            var logger = Substitute.For<ILogger<ProductService>>();

            productRepository.GetByIdAsync(mockProduct.Id).Returns(mockProduct);
            validator.ValidateAsync(Arg.Any<Product>()).Returns(Task.FromResult(new ValidationResult()));

            var productService = new ProductService(productRepository, branchProductRepository, validator, logger);

            // Act
            await productService.UpdateAsync(mockProduct.Id, updatedProduct);

            // Assert
            await branchProductRepository.Received(1).UpdateByProductIdAsync(mockProduct.Id, mockProduct.Name, updatedProduct.Category);
        }
    }
}
