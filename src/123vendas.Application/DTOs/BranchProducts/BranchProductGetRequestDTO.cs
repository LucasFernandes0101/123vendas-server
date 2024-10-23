namespace _123vendas.Application.DTOs.BranchProducts;

public class BranchProductGetRequestDTO
{
    public int? Id { get; set; }
    public int? ProductId { get; set; }
    public int? BranchId { get; set; }
    public string? ProductName { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Page { get; set; } = 1;
    public int MaxResults { get; set; } = 10;
}