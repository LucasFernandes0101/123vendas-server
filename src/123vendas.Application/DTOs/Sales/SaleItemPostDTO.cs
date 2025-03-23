namespace _123vendas.Application.DTOs.Sales;

public record SaleItemPostDTO
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal? Discount { get; init; }
}