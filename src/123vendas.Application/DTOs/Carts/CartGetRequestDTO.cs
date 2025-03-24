using _123vendas.Application.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace _123vendas.Application.DTOs.Carts;

public record CartGetRequestDTO : PagedRequestDTO
{
    public int? Id { get; init; }
    public int? UserId { get; init; }
    [FromQuery(Name = "_minDate")]
    public DateTimeOffset? MinDate { get; init; }
    [FromQuery(Name = "_maxDate")]
    public DateTimeOffset? MaxDate { get; init; }
}