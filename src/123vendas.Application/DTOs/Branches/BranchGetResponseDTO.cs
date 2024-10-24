namespace _123vendas.Application.DTOs.Branches;

public class BranchGetResponseDTO
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
}