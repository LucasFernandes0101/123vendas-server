using _123vendas.Domain.Base.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Domain.Base;

[ExcludeFromCodeCoverage]
public abstract class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}