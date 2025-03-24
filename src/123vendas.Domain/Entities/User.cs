using _123vendas.Domain.Base;
using _123vendas.Domain.Enums;

namespace _123vendas.Domain.Entities;

public class User : BaseEntity
{
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public UserName? Name { get; set; }
    public UserAddress? Address { get; set; }
    public string? Phone { get; set; }
    public UserStatus Status { get; set; }
    public UserRole Role { get; set; }
    public bool IsActive { get; set; }
}

public class UserName
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
}

public class UserAddress
{
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? Zipcode { get; set; }
    public UserAddressGeolocation? Geolocation { get; set; }
    public bool HasAddress { get; set; }
}

public class UserAddressGeolocation
{
    public string? Lat { get; set; }
    public string? Long { get; set; }
    public bool HasGeolocation { get; set; }
}