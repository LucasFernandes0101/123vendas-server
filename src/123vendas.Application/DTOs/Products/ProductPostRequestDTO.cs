using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Products;

public record ProductPostRequestDTO
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public ProductCategory Category { get; init; }
    public decimal BasePrice { get; init; }
    public bool IsActive { get; init; }
}