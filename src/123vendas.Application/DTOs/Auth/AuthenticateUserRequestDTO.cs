namespace _123vendas.Application.DTOs.Auth;

public record AuthenticateUserRequestDTO
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}