namespace _123vendas.Application.DTOs.Branches;

public class BranchGetRequestDTO
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public bool? IsActive { get; set; }
    public int Page { get; set; }
    public int MaxResults { get; set; }
}