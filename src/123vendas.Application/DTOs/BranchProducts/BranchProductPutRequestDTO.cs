namespace _123vendas.Application.DTOs.BranchProducts;

public record BranchProductPutRequestDTO
{
    public int Id { get; init; }
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public bool IsActive { get; init; }
}