namespace _123vendas.Application.DTOs.Customers;

public class CustomerPutRequestDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Document { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
}