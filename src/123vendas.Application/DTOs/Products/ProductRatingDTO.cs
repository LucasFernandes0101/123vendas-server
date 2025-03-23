namespace _123vendas.Application.DTOs.Products;

public record ProductRatingDTO
{
    public double? Rate { get; init; }
    public int? Count { get; init; }
}