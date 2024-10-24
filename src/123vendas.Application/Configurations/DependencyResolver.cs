using _123vendas.Application.Mappers.Branches;
using _123vendas.Application.Mappers.BranchProducts;
using _123vendas.Application.Mappers.Customers;
using _123vendas.Application.Mappers.Products;
using _123vendas.Application.Mappers.Sales;
using _123vendas.Application.Services;
using _123vendas.Domain.Entities;
using _123vendas.Domain.Interfaces.Integrations;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Domain.Interfaces.Services;
using _123vendas.Domain.Validators;
using _123vendas.Infrastructure.Contexts;
using _123vendas.Infrastructure.Integrations;
using _123vendas.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace _123vendas.Application.Configurations;

public static class DependencyResolver
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.ResolveAutoMapper();
        services.ResolveFluentValidators();
        services.ResolveRepositories();
        services.ResolveServices();

        services.AddSingleton<IRabbitMQIntegration, RabbitMQIntegration>();

        return services;
    }

    private static void ResolveRepositories(this IServiceCollection services)
    {
        services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING"))
            .EnableSensitiveDataLogging(true));

        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IBranchProductRepository, BranchProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<ISaleItemRepository, SaleItemRepository>();
    }

    private static void ResolveServices(this IServiceCollection services)
    {
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IBranchProductService, BranchProductService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ISaleService, SaleService>();
    }

    private static void ResolveAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BranchMapperProfile));
        services.AddAutoMapper(typeof(ProductMapperProfile));
        services.AddAutoMapper(typeof(BranchProductMapperProfile));
        services.AddAutoMapper(typeof(CustomerMapperProfile));
        services.AddAutoMapper(typeof(SaleMapperProfile));
    }

    private static void ResolveFluentValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<Branch>, BranchValidator>();
        services.AddScoped<IValidator<BranchProduct>, BranchProductValidator>();
        services.AddScoped<IValidator<Product>, ProductValidator>();
        services.AddScoped<IValidator<Customer>, CustomerValidator>();
        services.AddScoped<IValidator<Sale>, SaleValidator>();
        services.AddScoped<IValidator<SaleItem>, SaleItemValidator>();
    }
}