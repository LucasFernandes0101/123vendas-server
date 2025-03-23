using Microsoft.AspNetCore.Mvc;

namespace _123vendas.Application.DTOs.Common;

public record PagedRequestDTO
{
    [FromQuery(Name = "_page")]
    public int Page { get; set; } = 1;

    [FromQuery(Name = "_size")]
    public int Size { get; set; } = 10;

    [FromQuery(Name = "_order")]
    public string? OrderByClause { get; set; } = default;
}