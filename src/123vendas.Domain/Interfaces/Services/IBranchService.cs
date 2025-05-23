﻿using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;

namespace _123vendas.Domain.Interfaces.Services;

public interface IBranchService
{
    Task<PagedResult<Branch>> GetAllAsync(int? id = default, bool? isActive = default, string? name = default, DateTimeOffset? startDate = default, DateTimeOffset? endDate = default, int page = 1, int maxResults = 10, string? orderByClause = default);
    Task<Branch?> GetByIdAsync(int id);
    Task<Branch> CreateAsync(Branch request);
    Task<Branch> UpdateAsync(int id, Branch request);
    Task DeleteAsync(int id);
}