using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Products;

public record ProductGetDetailResponseDTO
{
    public int Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? Image { get; init; }
    public ProductCategory Category { get; init; }
    public decimal Price { get; init; }
    public ProductRatingDTO? Rating { get; init; }
    public bool IsActive { get; init; }
}