﻿using _123vendas.Application.Mappers.Branches;
using _123vendas.Application.Mappers.BranchProducts;
using _123vendas.Application.Mappers.Customers;
using _123vendas.Application.Mappers.Products;
using _123vendas.Application.Services;
using _123vendas.Application.Validators;
using _123vendas.Domain.Entities;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Domain.Interfaces.Services;
using _123vendas.Infrastructure.Contexts;
using _123vendas.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _123vendas.Application.Configurations;

public static class DependencyResolver
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.ResolveAutoMapper();
        services.ResolveFluentValidators();
        services.ResolveRepositories(configuration);
        services.ResolveServices();

        return services;
    }

    private static void ResolveRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("123VendasDB"))
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
    }

    private static void ResolveAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BranchMapperProfile));
        services.AddAutoMapper(typeof(ProductMapperProfile));
        services.AddAutoMapper(typeof(BranchProductMapperProfile));
        services.AddAutoMapper(typeof(CustomerMapperProfile));
    }

    private static void ResolveFluentValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<Branch>, BranchValidator>();
        services.AddScoped<IValidator<BranchProduct>, BranchProductValidator>();
        services.AddScoped<IValidator<Product>, ProductValidator>();
        services.AddScoped<IValidator<Customer>, CustomerValidator>();
    }
}