namespace _123vendas.Application.DTOs.Carts;

public record CartPostResponseDTO
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public DateTimeOffset Date { get; init; }
    public List<CartProductPostResponseDTO>? Products { get; init; }
}