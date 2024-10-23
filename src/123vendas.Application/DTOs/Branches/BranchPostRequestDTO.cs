namespace _123vendas.Application.DTOs.Branches;

public class BranchPostRequestDTO
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
}