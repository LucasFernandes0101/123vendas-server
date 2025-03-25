namespace _123vendas.Application.DTOs.Users;

public record UserAddressGeolocationDTO
{
    public string? Lat { get; set; }
    public string? Long { get; set; }
}