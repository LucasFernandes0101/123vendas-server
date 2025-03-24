namespace _123vendas.Application.DTOs.Sales;

public record SalePostRequestDTO
{
    public int UserId { get; init; }
    public int BranchId { get; init; }

    public List<SaleItemPostDTO>? Items { get; init; }
}