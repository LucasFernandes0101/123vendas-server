using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Infrastructure.Contexts;
using _123vendas.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _123vendas.Application.Configurations;

public static class DependencyResolver
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.ResolveRepositories(configuration);

        return services;
    }

    private static void ResolveRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("LivrariaDB"))
            .EnableSensitiveDataLogging(true));

        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IBranchProductRepository, BranchProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<ISaleItemRepository, SaleItemRepository>();
    }
}