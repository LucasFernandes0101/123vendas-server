namespace _123vendas.Application.DTOs.Sales;

public class SaleItemGetDTO
{
    public short Sequence { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
    public bool IsCancelled { get; set; }
}