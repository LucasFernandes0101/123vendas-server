namespace _123vendas.Application.DTOs.Auth;

public record AuthenticateUserResponseDTO
{
    public int Id { get; init; }
    public string? Token { get; init; }
    public string? Username { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string? Role { get; init; }
}