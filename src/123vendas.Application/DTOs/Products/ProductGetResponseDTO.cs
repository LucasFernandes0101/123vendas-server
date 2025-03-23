using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Products;

public record ProductGetResponseDTO
{
    public int Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? Name { get; init; }
    public ProductCategory Category { get; init; }
    public decimal BasePrice { get; init; }
    public bool IsActive { get; init; }
}