namespace _123vendas.Application.DTOs.Sales;

public record SalePostRequestDTO
{
    public int CustomerId { get; init; }
    public int BranchId { get; init; }

    public List<SaleItemPostDTO>? Items { get; init; }
}