namespace _123vendas.Application.DTOs.Carts;

public record CartGetResponseDTO
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public DateTimeOffset Date { get; init; }
    public List<CartProductGetResponseDTO>? Products { get; init; }
}