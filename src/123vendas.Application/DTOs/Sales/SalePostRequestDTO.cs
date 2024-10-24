namespace _123vendas.Application.DTOs.Sales;

public class SalePostRequestDTO
{
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }

    public List<SaleItemPostDTO>? Items { get; set; }
}