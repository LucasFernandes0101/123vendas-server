using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Products;

public record ProductPostRequestDTO
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public ProductCategory Category { get; init; }
    public decimal Price { get; init; }
    public string? Image { get; init; }
    public double Rating { get; init; }
    public int RateCount { get; init; }
    public bool IsActive { get; init; }
}