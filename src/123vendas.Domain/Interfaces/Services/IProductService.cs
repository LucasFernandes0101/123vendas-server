﻿using _123vendas.Domain.Entities;

namespace _123vendas.Domain.Interfaces.Services;

public interface IProductService
{
    Task<List<Product>> GetAllAsync(int? id, bool? isActive, string? name, DateTime? startDate, DateTime? endDate, int page = 1, int maxResults = 10);
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product request);
    Task<Product> UpdateAsync(int id, Product request);
    Task DeleteAsync(int id);
}