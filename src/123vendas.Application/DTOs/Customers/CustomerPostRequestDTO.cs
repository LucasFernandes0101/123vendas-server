namespace _123vendas.Application.DTOs.Customers;

public class CustomerPostRequestDTO
{
    public string? Name { get; set; }
    public string? Document { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
}