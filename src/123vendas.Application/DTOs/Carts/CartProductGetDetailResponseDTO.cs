﻿namespace _123vendas.Application.DTOs.Carts;

public record CartProductGetDetailResponseDTO
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}