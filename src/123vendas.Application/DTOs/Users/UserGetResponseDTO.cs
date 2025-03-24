using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Users;

public record UserGetResponseDTO
{
    public int Id { get; init; }
    public string? Username { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public UserRole Role { get; init; }
    public UserStatus Status { get; init; }
}