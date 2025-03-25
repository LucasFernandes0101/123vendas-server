namespace _123vendas.Application.DTOs.Carts;

public record CartProductPutResponseDTO
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}