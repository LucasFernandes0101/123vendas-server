using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.BranchProducts;

public class BranchProductGetResponseDTO
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int BranchId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public ProductCategory ProductCategory { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}