using AspNetCore.IQueryable.Extensions;

namespace _123vendas.Domain.Base;

public class BaseFilter : ICustomQueryable
{
    public Guid? Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int Page { get; set; } = 1;
    public int MaxResults { get; set; } = 10;
}