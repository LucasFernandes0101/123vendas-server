using _123vendas.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _123vendas.Application.Configurations;

public static class DependencyResolver
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddRepositories(configuration);

        return service;
    }

    private static void AddRepositories(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<SqlDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("LivrariaDB"))
            .EnableSensitiveDataLogging(true));
    }
}