namespace _123vendas.Application.DTOs.BranchProducts;

public record BranchProductPostRequestDTO
{
    public int BranchId { get; init; }
    public int ProductId { get; init; }
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public bool IsActive { get; init; }
}