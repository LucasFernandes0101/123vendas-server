namespace _123vendas.Application.DTOs.Sales;

public class SaleItemPostDTO
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal? Discount { get; set; }
}