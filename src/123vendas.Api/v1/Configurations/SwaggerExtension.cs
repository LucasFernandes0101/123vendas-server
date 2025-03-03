﻿using Microsoft.OpenApi.Models;

namespace _123vendas_server.v1.Configurations;

public static class SwaggerExtension
{
    public static void AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(type => type.FullName);
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API de Vendas 123Vendas",
                Description = "API para gerenciar vendas na 123Vendas, incluindo CRUD completo e registro de eventos de venda.",
                Version = "v1"
            });
        });
    }
}