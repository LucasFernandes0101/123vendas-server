using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Users;

public record UserPostRequestDTO
{
    public string? Username { get; init; }
    public string? Password { get; init; }
    public string? Phone { get; init; }
    public UserNameDTO? Name { get; set; }
    public UserAddressDTO? Address { get; init; }
    public string? Email { get; init; }
    public UserStatus? Status { get; init; }
    public UserRole? Role { get; init; }
}