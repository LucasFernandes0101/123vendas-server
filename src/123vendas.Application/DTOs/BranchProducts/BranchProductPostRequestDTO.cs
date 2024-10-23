namespace _123vendas.Application.DTOs.BranchProducts;

public class BranchProductPostRequestDTO
{
    public int BranchId { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
}