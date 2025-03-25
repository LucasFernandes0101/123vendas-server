using _123vendas.Domain.Entities;
using _123vendas.Domain.Enums;

namespace _123vendas.Application.Results.Users;

public record GetUserResult
{
    public int Id { get; init; }
    public string? Username { get; init; }
    public UserName? Name { get; init; }
    public UserAddress? Address { get; set; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public UserRole Role { get; init; }
    public UserStatus Status { get; init; }
}