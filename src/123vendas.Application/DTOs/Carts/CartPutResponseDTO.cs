namespace _123vendas.Application.DTOs.Carts;

public record CartPutResponseDTO
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public DateTimeOffset Date { get; init; }
    public List<CartProductPutResponseDTO>? Products { get; init; }
}