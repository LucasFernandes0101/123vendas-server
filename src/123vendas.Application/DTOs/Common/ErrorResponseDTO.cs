namespace _123vendas.Application.DTOs.Common;

public record ErrorResponseDTO
{
    public string? Type { get; init; }
    public string? Error { get; init; }
    public string? Detail { get; init; }
}