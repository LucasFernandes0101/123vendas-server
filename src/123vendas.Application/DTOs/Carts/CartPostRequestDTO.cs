namespace _123vendas.Application.DTOs.Carts;

public record CartPostRequestDTO
{
    public int UserId { get; init; }
    public DateTimeOffset Date { get; init; }
    public List<CartProductPostRequestDTO>? Products { get; init; }
}