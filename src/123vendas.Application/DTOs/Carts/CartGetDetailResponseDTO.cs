namespace _123vendas.Application.DTOs.Carts;

public record CartGetDetailResponseDTO
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public DateTimeOffset Date { get; init; }
    public IEnumerable<CartProductGetDetailResponseDTO>? Products { get; init; }
}