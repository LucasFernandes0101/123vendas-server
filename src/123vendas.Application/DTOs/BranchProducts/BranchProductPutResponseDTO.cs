﻿using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.BranchProducts;

public record BranchProductPutResponseDTO
{
    public int Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public int BranchId { get; init; }
    public int ProductId { get; init; }
    public string? ProductName { get; init; }
    public ProductCategory ProductCategory { get; init; }
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public bool IsActive { get; init; }
}