namespace _123vendas.Application.DTOs.BranchProducts;

public class BranchProductPutRequestDTO
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
}