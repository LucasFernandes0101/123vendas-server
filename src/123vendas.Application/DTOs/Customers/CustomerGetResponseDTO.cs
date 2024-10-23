namespace _123vendas.Application.DTOs.Customers;

public class CustomerGetResponseDTO
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Name { get; set; }
    public string? Document { get; set; }
    public bool IsActive { get; set; }
}